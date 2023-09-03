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
    private SphereCastVisualiser SCV;
    private RaycastHit hitData;

    private void Start()
    {
        SCV = GetComponent<SphereCastVisualiser>();
        //SCV.enabled = false;
    }
   void Update()
    {
        int hitResult = 0;
        if (Input.GetMouseButtonDown(0) && Time.time > timeForNextHit)
        {
            hitResult = 1;
            if (Physics.SphereCast(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane)), hitRadius, Camera.main.transform.forward, out hitData, hitRange))
            {
                hitResult = 2;
                IHit hitResponder = hitData.collider.gameObject.GetComponent<IHit>();
                if (hitResponder != null)
                {
                    hitResult = 3;
                    hitResponder.OnHit(hitData);
                }
            }
        }
        if (SCV.enabled && hitResult != 0)
        {
            float castRange = hitRange;
            Color color = new Color(0,0,0,0);
            if (hitResult == 1) 
            {
                color = new Color(0,1,0,0.5f); // green | miss
            }

            else if (hitResult == 2) 
            {
                color = new Color(0,0,1,0.5f); // blue | hit non-damagable
                castRange = hitData.distance;
            }

            else if (hitResult == 3) 
            {
                color = new Color(1,0,0,0.5f); // red | damaged
                castRange = hitData.distance;
            }
            SCV.Draw(color, castRange, hitRadius, hitData.collider, hitData.distance);
        }
    }
}