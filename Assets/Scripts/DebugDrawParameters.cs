using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDrawParameters
{
    public int result {get; set;}
    public Color color {get; set;}
    public float range {get; set;}
    public float time {get; set;}
    public float maxDistance {get; set;}
    public Vector3 origin {get; set;}
    public RaycastHit raycastHit {get; set;}
    public Collider closest {get; set;}
    public bool closeHit {get; set;}
    public bool farHit {get; set;}
    public float radius {get; set;}
}

