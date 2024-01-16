/*
FileName : SphereOverlapVisualiser.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 12 September 2023, 14:36:02
Description : Debug script for visualising weapon SphereOverlaps
*/
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereOverlapVisualiser : MonoBehaviour
{
    [SerializeField] private int maxCasts;
    [SerializeField] private GameObject spherePrefab;
    private Queue<GameObject> drawnObjects;
    public void Draw(Color color, float radius, Vector3 origin, Quaternion direction)
    {
        /* -------------------------------- Rendering ------------------------------- */
        GameObject sphere = Instantiate(spherePrefab, origin, direction);
        Renderer sphereRenderer = sphere.GetComponent<Renderer>();
        sphereRenderer.material.SetColor("_Color", color);
        sphere.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        /* -------------------------------- Old Casts ------------------------------- */
        drawnObjects.Enqueue(sphere);
        if (drawnObjects.Count > maxCasts)
        {
            Destroy(drawnObjects.Dequeue());
        }
    }
    [UsedImplicitly]
    private void Awake()
    {
        drawnObjects = new Queue<GameObject>();
    }
}