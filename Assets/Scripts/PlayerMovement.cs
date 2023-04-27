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
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float groundedTolerance = 0.11f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float sphereCastSize = 0.5f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    Vector3 playerVelocity = Vector3.zero;
    Vector3 verticalMove = Vector3.zero;
/* --------------------------------- Camera --------------------------------- */
    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float maxYLookAngle = 50f;
    [SerializeField] float minYLookAngle = -75f;
    private float yAxis = 0f;
    private float xAxis = 0f;

/* -------------------------------------------------------------------------- */
/*                             End Of Declarations                            */
/* -------------------------------------------------------------------------- */

    void Start()
    {
        cc = GetComponent<CharacterController>(); // caches cc ref to improve performance
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate() // called in update()
    {
    /* ---------------------------------- Input --------------------------------- */
        yAxis += (Input.GetAxis("Mouse Y") * lookSpeed);
        xAxis += (Input.GetAxis("Mouse X") * lookSpeed);
        yAxis = Mathf.Clamp(yAxis, maxYLookAngle * -1, minYLookAngle * -1); // prevents excessive camera rotation by clamping
    /* -------------------------------- Rotation -------------------------------- */
        gameObject.transform.eulerAngles = new Vector3(0, xAxis, 0); // playerBody X rotation
        playerEyes.transform.eulerAngles = new Vector3(yAxis,xAxis,0); // playerEyes X+Y rotation
    }

    private void Move() // called in update()
    {
        bool isGrounded;
    /* ---------------------------------- Input --------------------------------- */
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float forwardMove = Input.GetAxisRaw("Forward");
        verticalMove += new Vector3 (0,playerVelocity.y + gravity * Time.deltaTime,0); 
    /* ------------------------------- SphereCast ------------------------------- */
        Physics.SphereCast(transform.position - new Vector3(0,0.5f,0), sphereCastSize, transform.up * -1f, out RaycastHit hitData); // ground 

    /* ------------------------------ Ground Check ------------------------------ */
        if(hitData.distance == 0f) // contingency incase player is above void as sphereCast misses
        {
            isGrounded = false;
        }
        else if(hitData.distance <= groundedTolerance) 
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
       // Debug.Log($"{playerVelocity} - {verticalMove} - {hitData.distance} - {isGrounded}");
        cc.Move(transform.TransformDirection(playerVelocity) * Time.deltaTime); // 
        cc.Move(verticalMove * Time.deltaTime); // Y only
    }
    private void OnDrawGizmos() 
    {
        /* --------------------------- SphereCastDebugDraw -------------------------- */
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sphereCastSize);
    }
}