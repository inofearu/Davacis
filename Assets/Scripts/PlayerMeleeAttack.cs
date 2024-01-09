/*
FileName : SwordSwing.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 31 August 2023, 16:47:15
Description : Script to handle player sword.
*/
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack: MonoBehaviour
{
    public DebugDrawParameters debugInfo;
    private DamageModifier DamageModifier;
    /* --------------------------- Hit Characteristics -------------------------- */
    [SerializeField] float hitRadius; // placeholders until items
    [SerializeField] float hitCooldown;
    [SerializeField] float hitRange;
    [SerializeField] float hitDamage;
    [SerializeField] DamageModifier.DamageType hitType;
    private float nextHitTime;
    /* ------------------------------- SphereCast ------------------------------- */
    private RaycastHit hitData;
    /* ------------------------------ SphereOverlap ----------------------------- */
    private LayerMask rayHitLayers;

    [UsedImplicitly]
    private void Awake()
    {
        DamageModifier = GetComponent<DamageModifier>();
        rayHitLayers = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player"));
    }
    [UsedImplicitly]
    private void Update()
    {
        float hitTime = Time.time; // cache of time at frame
        int hitResult = 0;
        bool closeHit = false;
        bool farHit = false;
        Collider closest = null;
        Vector3 origin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        if (Input.GetMouseButtonDown(0) && Time.time > nextHitTime)
        {
            IHit hitResponder;
            hitTime = Time.time;
            nextHitTime = hitTime + hitCooldown;
            hitResult = 1; // miss
            List<Collider> closeEntities = new(Physics.OverlapSphere(origin, hitRadius, rayHitLayers));
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
                Physics.Raycast(origin, directionToEntity.normalized, out hitData);
                hitResponder = closest.gameObject.GetComponent<IHit>();

                hitResult = 2;
                if (hitResponder != null)
                {
                    hitResult = 3;
                    Physics.Raycast(origin, directionToEntity.normalized, out hitData);
                    hitResponder.OnHit(hitDamage, hitType);
                }
            }
            if (Physics.SphereCast(origin, hitRadius, Camera.main.transform.forward, out hitData, hitRange) && !closeHit)
            {
                farHit = true;
                hitResult = 2; // hit non-damagable
                hitResponder = hitData.collider.gameObject.GetComponent<IHit>();
                if (hitResponder != null)
                {
                    hitResult = 3; // hit damagable
                    hitResponder.OnHit(hitDamage, hitType);
                }
            }
        }
        debugInfo = new DebugDrawParameters
        {
            result = hitResult,
            maxDistance = hitRange,
            time = hitTime,
            radius = hitRadius,
            origin = origin,
            raycastHit = hitData,
            closest = closest,
            closeHit = closeHit,
            farHit = farHit
        };
    }
}