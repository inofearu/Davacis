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
    public bool showSpherecastDebug; // PLACEHOLDER: Implement in-game toggle
    private LineRenderer lineRenderer;
   void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > timeForNextHit)
        {
            if (showSpherecastDebug)
            {
                DrawSphereCast(Color.green);
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
    private void Awake()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>(); // PLACEHOLDER: Implement in-game toggle
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
    }
    private void DrawSphereCast(Color color)
    {
        Vector3 startPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        Vector3 endPoint = startPoint + Camera.main.transform.forward * hitRange;
        lineRenderer.widthMultiplier = hitRadius;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        Debug.Log($"{startPoint} - {endPoint}");
    }
}