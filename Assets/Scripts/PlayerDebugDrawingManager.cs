using JetBrains.Annotations;
using UnityEngine;

public class PlayerDebugDrawingManager : MonoBehaviour
{
    private PlayerMeleeAttack playerMeleeAttack;
    private PlayerMovement playerMovement;
    private PlayerMovementGroundChecker playerMovementGroundChecker;

    private SphereOverlapVisualiser sphereOverlapVisualiser;
    private SphereCastVisualiser sphereCastVisualiser;

    private AttackDebugParameters attackDebugInfo;
    private MovementDebugParameters movementDebugInfo;

    [SerializeField] private GameObject playerMovementGroundCheckObject;
    private MeshRenderer playerMovementGroundCheckAreaMeshRenderer;

    public bool raycastPrintEnabled;
    public bool raycastDrawEnabled;
    public bool movementPrintEnabled;
    public bool movementDrawEnabled;
    [UsedImplicitly]
    private void Awake()
    {
        playerMeleeAttack = GetComponent<PlayerMeleeAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMovementGroundChecker = playerMovementGroundCheckObject.GetComponent<PlayerMovementGroundChecker>();

        sphereOverlapVisualiser = GetComponent<SphereOverlapVisualiser>();
        sphereCastVisualiser = GetComponent<SphereCastVisualiser>();

        playerMovementGroundCheckAreaMeshRenderer = playerMovementGroundCheckObject.GetComponent<MeshRenderer>(); 
    }

    [UsedImplicitly]
    private void LateUpdate()
    {
        attackDebugInfo = playerMeleeAttack.debugInfo;
        movementDebugInfo = playerMovement.debugInfo;
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
                    sphereOverlapVisualiser.Draw(color, attackDebugInfo.radius, attackDebugInfo.origin, Quaternion.identity);
                }
                if (!attackDebugInfo.closeHit) // debug drawing of overlapSphere
                {
                    sphereCastVisualiser.Draw(color, drawnDistance, attackDebugInfo.radius, attackDebugInfo.origin, endPoint, Quaternion.FromToRotation(Vector3.up, (endPoint - attackDebugInfo.origin).normalized));
                }
            }
            if (raycastPrintEnabled)
            {
                string hitObject = "null";
                if (attackDebugInfo.raycastHit.collider is not null)
                {
                    hitObject = attackDebugInfo.raycastHit.collider.gameObject.name;
                }
                Debug.Log($@"
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
");
            }
        }
        if (movementPrintEnabled)
        {
            playerMovementGroundChecker.logActive = true;
            Debug.Log($@"
-----Movement-----
playerVelocity: {movementDebugInfo.playerVelocity}
verticalMove: {movementDebugInfo.verticalMove}
playerPosition: {movementDebugInfo.raycastHit.point}
isGrounded: {movementDebugInfo.isGrounded}
");
            foreach (string message in playerMovementGroundChecker.logMessage)
            {
                Debug.Log(message);
            }
            playerMovementGroundChecker.logMessage.Clear();
        }
        else
        {
            playerMovementGroundChecker.logActive = true;
        }
        if (movementDrawEnabled)
        {
            Color color;
            if (movementDebugInfo.isGrounded)
            {
                color = new Color(0, 1, 0, 0.5f);
            }
            else
            {
                color = new Color(1, 0, 0, 0.5f);
            }
            playerMovementGroundCheckAreaMeshRenderer.material.color = color;
        }
        playerMovementGroundCheckAreaMeshRenderer.enabled = movementDrawEnabled; // set 

    }
}