﻿using UnityEngine;

public class MovementDebugParameters
{
#pragma warning disable IDE1006
    public float overlapSphereRadius { get; set; }
    public Vector3 playerVelocity { get; set; }
    public Vector3 verticalMove { get; set; }
    public RaycastHit raycastHit { get; set; }
    public bool isGrounded { get; set; }
    public float groundedTolerance { get; set; }
    public Vector3 origin { get; set; }

#pragma warning restore IDE1006
}