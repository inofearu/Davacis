/*
FileName : SphereOverlapVisualiser.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 08 September 2023, 13:32:43
Description : Debug script for visualising weapon SphereOverlaps
*/
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereOverlapVisualiser : MonoBehaviour
{
    public int maxCasts; // TODO: implement in-game switch
    public GameObject spherePrefab;
    private Queue<GameObject> drawnObjects;
    public void Draw(Color color, float radius, Collider[] hitObj, float hitDist, float hitTime)
    {
        /* -------------------------------- Locations ------------------------------- */
        Vector3 startPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        Vector3 endPoint = startPoint + Camera.main.transform.forward.normalized;
        Vector3 centerPoint = (startPoint + endPoint) / 2;

        /* -------------------------------- Rendering ------------------------------- */
        GameObject sphere = Instantiate(spherePrefab, centerPoint, Quaternion.FromToRotation(Vector3.up, Camera.main.transform.forward));
        Renderer sphereRenderer = sphere.GetComponent<Renderer>();
        sphereRenderer.material.SetColor("_Color", color);
        sphere.transform.localScale = new Vector3(radius, radius, radius);

        /* -------------------------------- Old Casts ------------------------------- */
        drawnObjects.Enqueue(sphere);
        if (drawnObjects.Count > maxCasts)
        {
            Destroy(drawnObjects.Dequeue());
        }
        Debug.Log($"[{hitObj}], [{hitDist}], [{hitTime}]");
    }
    [UsedImplicitly]
    private void Awake()
    {
        drawnObjects = new Queue<GameObject>();
    }
}