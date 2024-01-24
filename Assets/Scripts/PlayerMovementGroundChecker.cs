using JetBrains.Annotations;
using UnityEngine;
using System.Collections.Generic;

public class PlayerMovementGroundChecker : MonoBehaviour
{
    List<Collider> InCollision;
    int layerMask;
    public List<string> logMessage;
    [UsedImplicitly]
    private void OnTriggerEnter(Collider other)
    {
        int colliderLayer = 1 << other.gameObject.layer;
        if ((colliderLayer & layerMask) != 0)
        {
            if (logActive) { logMessage.Add($"{other} entered trigger"); }
            InCollision.Add(other);
        }
    }
    [UsedImplicitly]
    private void OnTriggerExit(Collider other)
    {
        if (logActive) { logMessage.Add($"{other} left trigger"); }
        InCollision.Remove(other);
    }
    public bool checkGrounded()
    {
        
        if (InCollision.Count > 0)
        {
            if (logActive) { logMessage.Add("Grounded"); }
            return true;
        }
        if (logActive) { logMessage.Add("Ungrounded"); }
        return false;
    }
    [UsedImplicitly]
    private void Awake()
    {
        layerMask = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("Player"));
        InCollision = new List<Collider>();
        logMessage = new List<string>();
    }
}
