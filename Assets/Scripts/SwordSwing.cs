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
        RaycastHit hitData = OnClick();
        if (Input.GetMouseButtonDown(0) && Time.time > timeForNextHit)
        {
            if (hitData != null)
            {
                IHit hitResponder = hitCooldown.collidergameObject.GetComponent<IHit>();
                if (hitResponder != null)
                {
                    hitResponder.OnHit(hitData);
                }
            }
        }
    }

    private RaycastHit OnClick()
    {
        Physics.SphereCast(Camera.main.ScreenPointToRay(Input.mousePosition), hitRadius, transform.forward, out RaycastHit hitData, hitRange);
        timeForNextHit = Time.time + hitCooldown;
        return hitData;
    }
}
