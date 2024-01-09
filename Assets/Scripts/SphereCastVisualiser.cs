/*
FileName : SphereCastVisualiser.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 02 September 2023, 14:49:18
Description : Debug script for visualising weapon SphereCasts
*/
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastVisualiser : MonoBehaviour
{
    public int maxCasts;
    public GameObject capsulePrefab;
    private Queue<GameObject> drawnObjects;
    public void Draw(Color color, float range, float radius, Collider hitObj, Vector3 origin, Vector3 endPoint)
    {
        string hitObjName = "null"; //TODO: MOVE TO MANAGER
        /* -------------------------------- Location ------------------------------- */
        Vector3 centerPoint = (origin + endPoint) / 2;

        /* -------------------------------- Rendering ------------------------------- */
        GameObject capsule = Instantiate(capsulePrefab, centerPoint, Quaternion.FromToRotation(Vector3.up, (endPoint - origin).normalized));
        Renderer capsuleRenderer = capsule.GetComponent<Renderer>();
        capsuleRenderer.material.SetColor("_Color", color);
        capsule.transform.localScale = new Vector3(radius * 2, range / 2, radius * 2);

        /* -------------------------------- Old Casts ------------------------------- */
        drawnObjects.Enqueue(capsule);
        if (drawnObjects.Count > maxCasts)
        {
            Destroy(drawnObjects.Dequeue());
        }

        if (hitObj is not null)
        {
            hitObjName = hitObj.gameObject.name;
        }
        
    }
    [UsedImplicitly]
    private void Awake()
    {
        drawnObjects = new Queue<GameObject>();
    }
}