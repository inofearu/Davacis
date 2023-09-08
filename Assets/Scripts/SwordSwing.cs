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
using UnityEngine.InputSystem;

public class SwordSwing : MonoBehaviour
{
    /* --------------------------- Hit Characteristics -------------------------- */
    public float hitRadius;
    public float hitCooldown;
    public float hitRange;
    private float nextHitTime;
    /* ------------------------------- SphereCast ------------------------------- */
    private SphereCastVisualiser SCV;
    private RaycastHit hitData;
    private LayerMask rayHitLayers;

    [UsedImplicitly]
    private void Start()
    {
        SCV = GetComponent<SphereCastVisualiser>();
        SCV.enabled = true;

        rayHitLayers = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player"));
    }
    [UsedImplicitly]
    private void Update()
    {
        float hitTime = Time.time; // cache of time at frame
        int hitResult = 0;
        bool closeHit = false;
        if (Input.GetMouseButtonDown(0) && Time.time > nextHitTime)
        {
            Vector3 origin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
            hitTime = Time.time;
            nextHitTime = hitTime + hitCooldown;
            hitResult = 1; // miss 
            Collider[] closeEntities = Physics.OverlapSphere(origin, hitRadius, rayHitLayers);
            foreach (Collider entity in closeEntities)
            {
                Debug.Log(entity.gameObject.name);
                IHit hitResponder = entity.gameObject.GetComponent<IHit>();
                hitResult = 2;
                if (hitResponder != null)
                {
                    hitResult = 3;
                    Vector3 directionToEntity = entity.transform.position - origin;
                    Physics.Raycast(origin, directionToEntity.normalized, out hitData);
                    hitResponder.OnHit(hitData);
                }
                closeHit = true;
            }
            if (Physics.SphereCast(origin, hitRadius, Camera.main.transform.forward, out hitData, hitRange) && !closeHit)
            {
                hitResult = 2; // hit non-damagable
                IHit hitResponder = hitData.collider.gameObject.GetComponent<IHit>();
                if (hitResponder != null)
                {
                    hitResult = 3; // hit damagable
                    hitResponder.OnHit(hitData);
                }
            }
        }
        if (SCV.enabled && hitResult != 0) // debug drawing of spherecast path
        {
            float castRange = hitRange;
            Color color = new(0, 0, 0, 0);
            if (hitResult == 1)
            {
                color = new Color(0, 1, 0, 0.5f); // green | miss
            }
            else if (hitResult == 2)
            {
                color = new Color(0, 0, 1, 0.5f); // blue | hit non-damagable
                castRange = hitData.distance;
            }
            else if (hitResult == 3)
            {
                color = new Color(1, 0, 0, 0.5f); // red | damaged
                castRange = hitData.distance;
            }

            if (!closeHit)
            {
                SCV.Draw(color, castRange, hitRadius, hitData.collider, hitData.distance, hitTime);
            }
        }
    }
}