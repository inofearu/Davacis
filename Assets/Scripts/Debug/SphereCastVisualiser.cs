﻿/*
FileName : SphereCastVisualiser.cs 
Namespace: gDebug
FileType : C# Source File
Author : Christopher Huskinson
Created On : 02 September 2023, 14:49
Description : Debug script for visualising weapon SphereCasts
*/
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace gDebug
{
    public class SphereCastVisualiser : MonoBehaviour
    {
        [SerializeField] private int maxCasts;
        [SerializeField] private GameObject capsulePrefab;
        private Queue<GameObject> drawnObjects;
        public void Draw(Color color, float range, float radius, Vector3 origin, Vector3 endPoint, Quaternion direction)
        {
            /* -------------------------------- Location ------------------------------- */
            Vector3 centerPoint = (origin + endPoint) / 2;

            /* -------------------------------- Rendering ------------------------------- */
            GameObject capsule = Instantiate(capsulePrefab, centerPoint, direction);
            Renderer capsuleRenderer = capsule.GetComponent<Renderer>();
            capsuleRenderer.material.SetColor("_Color", color);
            capsule.transform.localScale = new Vector3(radius * 2, range / 2, radius * 2);

            /* -------------------------------- Old Casts ------------------------------- */
            drawnObjects.Enqueue(capsule);
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
}