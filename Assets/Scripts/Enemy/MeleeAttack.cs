/*
FileName : MeleeAttack.cs 
Namespace: Enemy
FileType : C# Source File
Author : Christopher Huskinson
Created On : 22nd September 2023, 13:51?
Description : Script to handle AI MeleeAttacks.
*/
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using Game.Damage;
using Item;

namespace Enemy
{
    public class MeleeAttack : MonoBehaviour
    {
        private DamageModifier DamageModifier; // TODO: implement this
        private Renderer objRenderer;
        /* --------------------------- Hit Characteristics -------------------------- */
        [SerializeField] private float radius;
        [SerializeField] private float hitCooldown;
        [SerializeField] private float maxDistance;
        [SerializeField] private int hitDamage;
        [SerializeField] private DamageModifier.DamageType hitType;
        [SerializeField] private float hitHeightOffset;
        private float nextHitTime;
        /* ------------------------------- SphereCast ------------------------------- */
        private gDebug.SphereCastVisualiser SCV;
        private gDebug.OverlapSphereVisualiser OSV;
        private RaycastHit raycastHit;
        /* ------------------------------ SphereOverlap ----------------------------- */
        private LayerMask rayHitLayers;

        [UsedImplicitly]
        private void Awake()
        {
            DamageModifier = GetComponent<DamageModifier>();
            SCV = GetComponent<gDebug.SphereCastVisualiser>();
            OSV = GetComponent<gDebug.OverlapSphereVisualiser>();
            objRenderer = GetComponent<Renderer>();
            rayHitLayers = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Enemy"));
        }
        [UsedImplicitly]
        private void Update()
        {
            float hitTime;
            int result = 0;
            bool closeHit = false;
            bool farHit = false;
            Collider closest = null;
            Vector3 origin = gameObject.transform.position + new Vector3(0, objRenderer.bounds.size.y - hitHeightOffset, 0);
            if (Time.time > nextHitTime)
            {
                IHit hitResponder;
                hitTime = Time.time;
                nextHitTime = hitTime + hitCooldown;
                result = 1; // miss
                List<Collider> closeEntities = new(Physics.OverlapSphere(origin, radius, rayHitLayers));
                if (closeEntities.Count > 0)
                {
                    closeHit = true;
                    closest = closeEntities[0];
                    closeEntities.RemoveAt(0);
                }
                foreach (Collider entity in closeEntities)
                {
                    if (Vector3.Distance(entity.transform.position, origin) < Vector3.Distance(closest.transform.position, origin))
                    {
                        closest = entity;
                    }
                }
                if (closest != null)
                {
                    Vector3 directionToEntity = closest.transform.position - origin;
                    Physics.Raycast(origin, directionToEntity.normalized, out raycastHit);
                    hitResponder = closest.gameObject.GetComponent<IHit>();

                    result = 2;
                    if (hitResponder != null)
                    {
                        result = 3;
                        Physics.Raycast(origin, directionToEntity.normalized, out raycastHit);
                        hitResponder.OnHit(hitDamage, hitType);
                    }
                }
                if (Physics.SphereCast(origin, radius, gameObject.transform.forward, out raycastHit, maxDistance) && !closeHit)
                {
                    farHit = true;
                    result = 2; // hit non-damagable
                    hitResponder = raycastHit.collider.gameObject.GetComponent<IHit>();
                    if (hitResponder != null)
                    {
                        result = 3; // hit damagable
                        hitResponder.OnHit(hitDamage, hitType);
                    }
                }
            }
            if (result != 0)
            {
                Color color;
                if (result == 2)
                {
                    color = new Color(0, 0, 1, 0.5f); // blue | hit non-damagable
                }
                else if (result == 3)
                {
                    color = new Color(1, 0, 0, 0.5f); // red | hit damagable
                }
                else
                {
                    color = new Color(0, 1, 0, 0.5f); // green | miss 
                }
                if (SCV.enabled && !closeHit) // debug drawing of spherecast path
                {
                    float castRange;
                    if (raycastHit.collider == null)
                    {
                        castRange = maxDistance;
                    }
                    else
                    {
                        castRange = raycastHit.distance;
                    }
                    Vector3 endPoint = origin + (gameObject.transform.forward.normalized * castRange);
                    //   SCV.Draw(color, castRange, radius, raycastHit.collider, hitTime, origin, endPoint); // TODO: GET INTO A MANAGER
                    //TODO: UNCOMMENT
                }
                if (OSV.enabled && !farHit) // debug drawing of sphereOverlap
                {
                    //    OSV.Draw(color, raycastHit.distance, radius, closest, hitTime, origin); // TODO: GET INTO A MANAGER
                    //TODO: UNCOMMENT
                }
            }
        }
    }
}