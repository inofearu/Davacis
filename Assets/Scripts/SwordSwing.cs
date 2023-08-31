using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordSwing : MonoBehaviour
{
    public float hitRadius;
    public float hitCooldown;
    public float hitRange;
    private float timeForNextHit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > timeForNextHit)
        {
            Debug.Log("1");
            if (Physics.SphereCast(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10.0f)), hitRadius, transform.forward, out RaycastHit hitData, hitRange))
            {
                Debug.Log("2");
                IHit hitResponder = hitData.collider.gameObject.GetComponent<IHit>();
                if (hitResponder != null)
                {
                    Debug.Log("3");
                    hitResponder.OnHit(hitData);
                }
            }
        }
    }
}