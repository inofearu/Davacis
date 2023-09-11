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
    private SphereOverlapVisualiser SOV;
    private RaycastHit hitData;
    /* ------------------------------ SphereOverlap ----------------------------- */
    private LayerMask rayHitLayers;
    private Collider[] closeEntities;
    //private Vector3 playerSize;

    [UsedImplicitly]
    private void Start()
    {
        SCV = GetComponent<SphereCastVisualiser>();
        SCV.enabled = true;

        rayHitLayers = Physics.DefaultRaycastLayers & ~(1 << LayerMask.NameToLayer("Player"));
        //playerSize = GameObject.Find("Player Body").GetComponent<Renderer>().bounds.size;
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
            closeEntities = Physics.OverlapSphere(origin, hitRadius, rayHitLayers);

            Dictionary<int, float> entitiesDistance = new Dictionary<int, float>(); // key = index
            int index = 0;
            foreach (Collider entity in closeEntities)
            {
                Vector3 directionToEntity = entity.transform.position - origin;
                Physics.Raycast(origin, directionToEntity.normalized, out hitData);
                entitiesDistance.Add(index, hitData.distance);
                index++;
            }
            List<KeyValuePair<int, float>> entitiesDistanceSort = new List<KeyValuePair<int, float>>(entitiesDistance);
            entitiesDistanceSort.Sort((x, y) => x.Value.CompareTo(y.Value));
            entity = entitiesDistance[entitiesDistanceSort[0]].Key; // this is fucked

            IHit hitResponder = entity.gameObject.GetComponent<IHit>();
            hitResult = 2;
            if (hitResponder != null)
            {
                hitResult = 3;
                Physics.Raycast(origin, directionToEntity.normalized, out hitData);
                hitResponder.OnHit(hitData);
            }
            closeHit = true;
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
        if (hitResult != 0)
        {
            Color color = new(0, 0, 0, 0);
            if (hitResult == 1)
            {
                color = new Color(0, 1, 0, 0.5f); // green | miss
            }
            else if (hitResult == 2)
            {
                color = new Color(0, 0, 1, 0.5f); // blue | hit non-damagable
            }
            else if (hitResult == 3)
            {
                color = new Color(1, 0, 0, 0.5f); // red | 
            }
            if (SCV.enabled && !closeHit) // debug drawing of spherecast path
            {
                float castRange;
                if (hitData.distance == Mathf.Infinity)
                {
                    castRange = hitRange;
                }
                else
                {
                    castRange = hitData.distance;
                }
                if (!closeHit)
                {
                    SCV.Draw(color, castRange, hitRadius, hitData.collider, hitData.distance, hitTime);
                }
            }
            if (SOV.enabled && closeHit) // debug drawing of sphereOverlap
            {
                SOV.Draw(color, hitRadius, closeEntities, hitData.distance, hitTime);
            }
        }
    }
}