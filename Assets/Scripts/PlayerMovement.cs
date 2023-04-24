/*
FileName : PlayerMovement.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 29th March 2023, 15:03:04
Description : Script to handle player movement.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
/* --------------------------------- Objects -------------------------------- */
    [SerializeField] GameObject playerEyes;
    private CharacterController cc;
/* -------------------------------- Movement -------------------------------- */
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 7.5f;
    Vector3 playerVelocity = Vector3.zero;
    Vector3 verticalMove = Vector3.zero;
/* --------------------------------- Camera --------------------------------- */
    [SerializeField] float lookSpeed = 1f;
    [SerializeField] float maxYLookAngle = 30f;
    [SerializeField] float minYLookAngle = -30f;
    private float yAxis = 0f;
    private float xAxis = 0f;

/* -------------------------------------------------------------------------- */
/*                             End Of Declarations                            */
/* -------------------------------------------------------------------------- */

    void Start() // Start is called before the first frame update
    {
        cc = GetComponent<CharacterController>(); // caches cc ref to improve performance
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Update() // Update is called once per frame
    {
        Rotate();
        Move();
    }

    private void Rotate() // called in update()
    {
    /* ---------------------------------- Input --------------------------------- */
        yAxis += (Input.GetAxis("Mouse Y") * lookSpeed);
        xAxis += (Input.GetAxis("Mouse X") * lookSpeed);
        yAxis = Mathf.Clamp(yAxis, maxYLookAngle * -1, minYLookAngle * -1); // values flipped so that you dont need to provide a - value for max

    /* ------------------------------- Application ------------------------------ */
        gameObject.transform.eulerAngles = new Vector3(0, xAxis, 0); // playerBody X rotation
        playerEyes.transform.eulerAngles = new Vector3(yAxis,xAxis,0); // playerEyes X+Y rotation
    }

    private void Move() // called in update()
    {
    /* ---------------------------------- Input --------------------------------- */
        bool isGrounded;
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float forwardMove = Input.GetAxisRaw("Forward");
        verticalMove += new Vector3 (0,playerVelocity.y + -9.81f * Time.deltaTime,0);
    /* --------------------------------- RayCast -------------------------------- */
        Ray groundRay = new Ray(transform.position, transform.up * -1.1f);
        Physics.Raycast(groundRay, out RaycastHit hitData);
    /* ------------------------------ Ground Check ------------------------------ */
        if(hitData.distance <= 1.1f) 
        {
            isGrounded = true;
        }
        else 
        {
            isGrounded = false;
        }
    /* ---------------------------------- Jump ---------------------------------- */
        if(isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                verticalMove += new Vector3(0,jumpHeight,0);
            }
            else
            {
                verticalMove = Vector3.zero;
            }
        }
    /* --------------------------------- Sprint --------------------------------- */
        if(Input.GetKey(KeyCode.LeftShift))
        {
            playerVelocity = new Vector3 (horizontalMove,0,forwardMove).normalized * sprintSpeed;
        }
        else
        {
            playerVelocity = new Vector3 (horizontalMove,0,forwardMove).normalized * walkSpeed;
        }
    /* --------------------------------- Output --------------------------------- */
        cc.Move(transform.TransformDirection(playerVelocity) * Time.deltaTime); // 
        cc.Move(verticalMove * Time.deltaTime); // Y only
    }
}