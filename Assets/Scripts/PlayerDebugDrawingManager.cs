using JetBrains.Annotations;
using UnityEngine;

public class PlayerDebugDrawingManager : MonoBehaviour
{
    private PlayerMeleeAttack PMA;
    private SphereOverlapVisualiser SOV;
    private SphereCastVisualiser SCV;
    private DebugDrawParameters debugInfo;
    public bool raycastPrintEnabled;
    public bool raycastDrawEnabled;
    [UsedImplicitly]
    private void Awake()
    {
        PMA = GetComponent<PlayerMeleeAttack>();
        SOV = GetComponent<SphereOverlapVisualiser>();
        SCV = GetComponent<SphereCastVisualiser>();
    }

    [UsedImplicitly]
    private void LateUpdate()
    {
        debugInfo = PMA.debugInfo;
        float drawnDistance;
        if (debugInfo.result != 0 && (raycastDrawEnabled || raycastPrintEnabled))
        {
            Color color;
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
            if (debugInfo.raycastHit.collider == null)
            {
                drawnDistance = debugInfo.maxDistance; // if we didnt hit anything, use the attack's maximum distance
            }
            else
            {
                drawnDistance = debugInfo.raycastHit.distance; // else we use the distance of the object 
            }
            Vector3 endPoint = debugInfo.origin + (Camera.main.transform.forward.normalized * drawnDistance);
            if (raycastDrawEnabled)
            {
                SCV.Draw(color, drawnDistance, debugInfo.radius, debugInfo.origin, endPoint);
                if (debugInfo.closeHit) // debug drawing of sphereOverlap
                {
                    SOV.Draw(color, debugInfo.radius, debugInfo.origin);
                }
            }
            if (raycastPrintEnabled)
            {
                /*string logMessage = $@"
-----Raycasts-----
    --Space--
    Origin: {debugInfo.origin}
    endPoint: {endPoint}
    Distance: {drawnDistance}
    Radius: {debugInfo.radius}
    --GameObject--
    Name: {debugInfo.raycastHit.collider.gameObject.name}
    --Time--
    Time: {debugInfo.time}
";
                Debug.Log(logMessage);*/
                Debug.Log(debugInfo.origin);
                Debug.Log(endPoint);
                Debug.Log(drawnDistance);
                Debug.Log(debugInfo.radius);
                Debug.Log(debugInfo.raycastHit.collider.gameObject.name);
                Debug.Log(debugInfo.time);
            }
        }
    }
}
// Structuring issues with not wanting the main attack file to pass to the debug file so that they are not coupled, want the debug file to pull instead. Choosing parameter approach obj.