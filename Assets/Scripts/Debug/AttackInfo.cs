/*
FileName : AttackInfo.cs 
Namespace: gDebug.Parameters
FileType : C# Source File
Author : Christopher Huskinson
Created On : 9th January 2024, 13:23
Description : Contains variables for attacks passed out of main files to debug ones.
*/
using UnityEngine;

namespace gDebug.Parameters
{
    public class AttackInfo
    {
#pragma warning disable IDE1006 // Naming Styles
        public int result { get; set; }
        public Color color { get; set; }
        public float range { get; set; }
        public float time { get; set; }
        public float maxDistance { get; set; }
        public Vector3 origin { get; set; }
        public RaycastHit raycastHit { get; set; }
        public Collider closest { get; set; }
        public bool closeHit { get; set; }
        public bool farHit { get; set; }
        public float radius { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}

