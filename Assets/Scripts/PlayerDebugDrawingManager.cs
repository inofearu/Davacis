using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebugDrawingManager : MonoBehaviour
{
    PlayerMeleeAttack PMA;
    SphereOverlapVisualiser SOV;
    SphereCastVisualiser SCV;
    DebugDrawParameters debugInfo;
    public bool sovPrint;
    public bool scvPrint;

    [UsedImplicitly]
    private void Awake()
    {
        PMA = GetComponent<PlayerMeleeAttack>();
        SOV = GetComponent<SphereOverlapVisualiser>();
        SCV = GetComponent<SphereCastVisualiser>();
        if (PMA == null)
        {
            Debug.LogError("PMA is null");
        }
    }
    [UsedImplicitly]
    private void LateUpdate()
    {
        float castRange = 12345.12345f; //! this should never print
        Vector3 endPoint = new Vector3(12345, 12345, 12345); //! this should never print
        debugInfo = PMA.debugInfo;
        if (debugInfo.result != 0)
        {
            Color color = new(0, 0, 0, 0);
            if (debugInfo.result == 2)
            {
                color = new Color(0, 0, 1, 0.5f); // blue | hit non-damagable
            }
            else if (debugInfo.result == 3)
            {
                color = new Color(1, 0, 0, 0.5f); // red | hit damagable
            }
            else
            {
                color = new Color(0, 1, 0, 0.5f); // green | miss 
            }
            if ((SCV.enabled || SOV.enabled || sovPrint || scvPrint) && !debugInfo.closeHit) //? this is incredibly awful 
            {
                if (debugInfo.raycastHit.collider == null)
                {
                    castRange = debugInfo.maxDistance;
                }
                else
                {
                    castRange = debugInfo.raycastHit.distance;
                }
                endPoint = debugInfo.origin + (Camera.main.transform.forward.normalized * castRange);
                SCV.Draw(color, castRange, debugInfo.radius, debugInfo.raycastHit.collider, debugInfo.origin, endPoint);
            }
            if (SOV.enabled && !debugInfo.farHit) // debug drawing of sphereOverlap
            {
                SOV.Draw(color, debugInfo.radius, debugInfo.origin);
            }
            if (sovPrint) 
            { 
                Debug.Log($"Origin: [{debugInfo.origin}], Hit Object: [{debugInfo.raycastHit.collider.gameObject.name}], Distance: [{castRange}]Radius: [{debugInfo.radius}], Hit Time: [{debugInfo.time}]");
            }
            if (scvPrint)
            {
                Debug.Log($"[Origin: {debugInfo.origin} - End: {endPoint}], Hit Object: [{debugInfo.raycastHit.collider.gameObject.name}], Distance: [{castRange}], Hit Time: [{debugInfo.time}]");
            }
        }
    }
} //TODO: Standardise the contents of the prints
  // Structuring issues with not wanting the main attack file to pass to the debug file so that they are not coupled, want the debug file to pull instead. Choosing parameter approach obj.
  // no longer player only manager