/*
FileName : SphereCastVisualiser.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 02 September 2023, 14:49:18
Description : Debug script for visualising weapon spherecasts
*/
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastVisualiser : MonoBehaviour
{
    public int maxCasts; // TODO: implement in-game switch
    public GameObject capsulePrefab;
    private Queue<GameObject> drawnObjects;
    public void Draw(Color color, float range, float radius, Collider hitObj, float hitDist, float hitTime)
    {
        /* -------------------------------- Locations ------------------------------- */
        Vector3 startPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        Vector3 endPoint = startPoint + (Camera.main.transform.forward.normalized * range);
        Vector3 centerPoint = (startPoint + endPoint) / 2;

        /* -------------------------------- Rendering ------------------------------- */
        GameObject capsule = Instantiate(capsulePrefab, centerPoint, Quaternion.FromToRotation(Vector3.up, Camera.main.transform.forward));
        Renderer capsuleRenderer = capsule.GetComponent<Renderer>();
        capsuleRenderer.material.SetColor("_Color", color);
        capsule.transform.localScale = new Vector3(radius * 2, range / 2, radius * 2);

        /* -------------------------------- Old Casts ------------------------------- */
        drawnObjects.Enqueue(capsule);
        if (drawnObjects.Count > maxCasts)
        {
            Destroy(drawnObjects.Dequeue());
        }
        Debug.Log($"[{startPoint} - {endPoint}], [{hitObj}], [{hitDist}], [{hitTime}]");
    }
    [UsedImplicitly]
    private void Awake()
    {
        drawnObjects = new Queue<GameObject>();
    }
}