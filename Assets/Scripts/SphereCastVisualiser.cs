using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastVisualiser : MonoBehaviour
{
    public GameObject capsulePrefab;
    public void Draw(Color color, float range, float radius, Collider hitObj)
    {
        Vector3 startPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        Vector3 endPoint = startPoint + Camera.main.transform.forward.normalized * range;
        Vector3 centerPoint = (startPoint + endPoint) / 2;

        GameObject capsule = Instantiate(capsulePrefab, centerPoint, Quaternion.FromToRotation(Vector3.up, Camera.main.transform.forward));
        Renderer capsuleRenderer = capsule.GetComponent<Renderer>();
        capsuleRenderer.material.SetColor("_Color", color);
        capsule.transform.localScale = new Vector3(radius * 2, range / 2, radius * 2);
        Debug.Log($"|{startPoint} - {endPoint}|, {hitObj}");
    }
}