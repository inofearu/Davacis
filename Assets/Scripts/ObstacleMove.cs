/*
FileName : ObstacleMove.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 31 August 2023, 16:47:15
Description : Script to handle debug moving of GameObject
*/
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    /* --------------------------------- Objects -------------------------------- */
    private Rigidbody rb;
    /* -------------------------------- Movement -------------------------------- */
    private Vector3 move;
    private int direction;
    /* -------------------------------- Location -------------------------------- */
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;
    /* ---------------------------------- Time ---------------------------------- */
    private float time;
    [SerializeField] private float move_for;
    private float move_until;
    [UsedImplicitly]
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        time = Time.time;
        move_until = time + move_for;
        direction = 1;
    }

    [UsedImplicitly]
    private void FixedUpdate()
    {
        time = Time.time;
        if (time > move_until) // swap direction
        {
            direction *= -1;
            move_until = time + move_for;
        }
        if (direction == 1) // backwards
        {
            move = new Vector3(x, y, z) * -1;
        }
        else // forwards
        {
            move = new Vector3(x, y, z);
        }
        rb.MovePosition(transform.position + (move * Time.deltaTime));
    }
}
