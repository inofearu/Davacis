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
    public bool showSpherecastDebug;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > timeForNextHit)
        {
            if (showSpherecastDebug)
            {
                DrawSphereCast(Color.white);
            }

            if (Physics.SphereCast(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane)), hitRadius, transform.forward, out RaycastHit hitData, hitRange))
            {
                if (showSpherecastDebug)
                {
                    DrawSphereCast(Color.yellow);
                }

                IHit hitResponder = hitData.collider.gameObject.GetComponent<IHit>();
                if (hitResponder != null)
                {
                    if (showSpherecastDebug)
                    {
                        DrawSphereCast(Color.red);
                    }
                    hitResponder.OnHit(hitData);
                }
            }
        }
    }
    private void DrawSphereCast(Color color)
    {
        Vector3 startPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        Vector3 endPoint = startPoint + Camera.main.transform.forward * hitRange;
        Debug.DrawLine(startPoint, endPoint, color, 600, false);
        Debug.Log($"{startPoint} - {endPoint}");
    }
}