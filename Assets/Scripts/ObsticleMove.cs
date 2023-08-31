using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleMove : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float x;
    [SerializeField] float y;
    [SerializeField] float z;
    private Vector3 move;
    private float time; 
    [SerializeField] float move_for;
    private float move_until;
    private int direction;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        time = Time.time;
        move_until = time + move_for;
        direction = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time = Time.time;
        if(time > move_until)
        {
            direction = direction * -1;
            move_until = time + move_for;
        }
        if(direction == 1)
        {
            move = new Vector3(x, y, z) * -1; 
        }
        else
        {  
            move = new Vector3(x, y, z); 
        }
        rigidbody.MovePosition(transform.position + move * Time.deltaTime);

    }
}
