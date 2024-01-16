using JetBrains.Annotations;
using UnityEngine;

public class PlayerDebugDrawingManager : MonoBehaviour
{
    private PlayerMeleeAttack PMA;
    private PlayerMovement PM;
    private SphereOverlapVisualiser SOV;
    private SphereCastVisualiser SCV;
    private AttackDebugParameters attackDebugInfo;
    private MovementDebugParameters movementDebugInfo;
    public bool raycastPrintEnabled;
    public bool raycastDrawEnabled;
    public bool movementPrintEnabled;
    [UsedImplicitly]
    private void Awake()
    {
        PMA = GetComponent<PlayerMeleeAttack>();
        PM = GetComponent<PlayerMovement>();
        SOV = GetComponent<SphereOverlapVisualiser>();
        SCV = GetComponent<SphereCastVisualiser>();
    }

    [UsedImplicitly]
    private void LateUpdate()
    {
        attackDebugInfo = PMA.debugInfo;
        movementDebugInfo = PM.debugInfo;
        float drawnDistance;
        if (attackDebugInfo.result != 0 && (raycastDrawEnabled || raycastPrintEnabled))
        {
            Color color;
            if (attackDebugInfo.result == 2)
            {
                color = new Color(0, 0, 1, 0.5f); // blue | hit non-damagable
            }
            else if (attackDebugInfo.result == 3)
            {
                color = new Color(1, 0, 0, 0.5f); // red | hit damagable
            }
            else
            {
                color = new Color(0, 1, 0, 0.5f); // green | miss 
            }
            if (attackDebugInfo.raycastHit.collider == null)
            {
                drawnDistance = attackDebugInfo.maxDistance; // if we didnt hit anything, use the attack's maximum distance
            }
            else
            {
                drawnDistance = attackDebugInfo.raycastHit.distance; // else we use the distance of the object 
            }
            Vector3 endPoint = attackDebugInfo.origin + (Camera.main.transform.forward.normalized * drawnDistance);
            if (raycastDrawEnabled)
            {
                if (!attackDebugInfo.farHit)
                {
                    SOV.Draw(color, attackDebugInfo.radius, attackDebugInfo.origin, Quaternion.identity);
                }
                if (!attackDebugInfo.closeHit) // debug drawing of sphereOverlap
                {
                    SCV.Draw(color, drawnDistance, attackDebugInfo.radius, attackDebugInfo.origin, endPoint, Quaternion.FromToRotation(Vector3.up, (endPoint - attackDebugInfo.origin).normalized));
                }
            }
            if (raycastPrintEnabled)
            {
                string hitObject = "null";
                if (attackDebugInfo.raycastHit.collider is not null)
                {
                    hitObject = attackDebugInfo.raycastHit.collider.gameObject.name;
                }
                string logMessage = $@"
-----Raycasts-----
    farHit: {attackDebugInfo.closeHit}
    closeHit: {attackDebugInfo.closeHit}
    --Space--
    Origin: {attackDebugInfo.origin}
    endPoint: {endPoint}
    Distance: {drawnDistance}
    Radius: {attackDebugInfo.radius}
    --GameObject--
    Name: {hitObject}
    --Time--
    Time: {attackDebugInfo.time}
";
                Debug.Log(logMessage);
            }
        }
        if (movementPrintEnabled)
        {
            string logMessage = $@"
-----Movement-----
playerVelocity: {movementDebugInfo.playerVelocity}
verticalMove: {movementDebugInfo.verticalMove}
playerPosition: {movementDebugInfo.raycastHit.point}
sphereCastSize: {movementDebugInfo.sphereCastSize}
isGrounded: {movementDebugInfo.isGrounded}
";
            Debug.Log(logMessage);
        }
    }
}
// Structuring issues with not wanting the main attack file to pass to the debug file so that they are not coupled, want the debug file to pull instead. Choosing parameter approach obj.