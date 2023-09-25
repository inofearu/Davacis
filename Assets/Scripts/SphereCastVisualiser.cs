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
    public int maxCasts; // TODO: implement in-game switch
    public GameObject capsulePrefab;
    private Queue<GameObject> drawnObjects;
    public void Draw(Color color, float range, float radius, Collider hitObj, float hitTime, Vector3 startPoint, Vector3 endPoint)
    {
        string hitObjName = "null";
        /* -------------------------------- Location ------------------------------- */
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

        if (hitObj is not null)
        {
            hitObjName = hitObj.gameObject.name;
        }
        Debug.Log($"[Start: {startPoint} - End: {endPoint}], Hit Object: [{hitObjName}], Range: [{range}], Hit Time: [{hitTime}]");
    }
    [UsedImplicitly]
    private void Awake()
    {
        drawnObjects = new Queue<GameObject>();
    }
}