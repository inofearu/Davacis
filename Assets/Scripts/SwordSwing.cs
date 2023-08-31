using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordSwing : MonoBehaviour
{
    public float swordRange;
    public float hitRadius;
    public float hitCooldown;
    public float hitRange;
    private float timeForNextHit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > timeForNextHit)
        {
            if (Physics.SphereCast(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10.0f)), hitRadius, transform.forward, out RaycastHit hitData, hitRange))
            {
                IHit hitResponder = hitCooldown.collidergameObject.GetComponent<IHit>();
                if (hitResponder != null)
                {
                    hitResponder.OnHit(hitData);
                }
            }
        }
    }
}
// v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10.0f));