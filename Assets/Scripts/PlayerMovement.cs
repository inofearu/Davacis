/*
FileName : PlayerMovement.cs 
FileType : C# Source File
Author : Christopher Huskinson
Created On : 29th March 2023, 15:03:04
Description : Script to handle player movement.
*/
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public MovementDebugParameters debugInfo;
    /* --------------------------------- Objects -------------------------------- */
    [SerializeField] private GameObject playerEyes;
    [SerializeField] private PlayerMovementGroundChecker GroundChecker;
    private CharacterController cc;
    /* -------------------------------- Movement -------------------------------- */
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private bool isGrounded;
    private Vector3 playerVelocity = Vector3.zero;
    private Vector3 verticalMove = Vector3.zero;
    /* --------------------------------- Camera --------------------------------- */
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float maxYLookAngle = 50f;
    [SerializeField] private float minYLookAngle = -75f;
    private float yAxis; // affected by player mouse
    private float xAxis; // affected by player mouse

    [UsedImplicitly]
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // captures cursor
    }
    [UsedImplicitly]
    private void Update()
    {
        isGrounded = GroundChecker.CheckGrounded();
        Rotate();
        Move();
        CreateDebugInfo();
    }
    private void Rotate()
    {
        /* ---------------------------------- Input --------------------------------- */
        yAxis += (Input.GetAxis("Mouse Y") * lookSpeed);
        xAxis += (Input.GetAxis("Mouse X") * lookSpeed);
        yAxis = Mathf.Clamp(yAxis, maxYLookAngle * -1, minYLookAngle * -1); // prevents excessive camera rotation by clamping
        /* -------------------------------- Rotation -------------------------------- */
        gameObject.transform.eulerAngles = new Vector3(0, xAxis, 0); // playerBody X rotation
        playerEyes.transform.eulerAngles = new Vector3(yAxis, xAxis, 0); // playerEyes X+Y rotation
    }

    private void Move()
    {
        /* ---------------------------------- Input --------------------------------- */
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float forwardMove = Input.GetAxisRaw("Forward");
        verticalMove += new Vector3(0, playerVelocity.y + (gravity * Time.deltaTime), 0);
        /* ---------------------------------- Jump ---------------------------------- */
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                verticalMove = new Vector3(0, jumpHeight, 0);
            }
            else
            {
                verticalMove = Vector3.zero;
            }
        }
        /* --------------------------------- Sprint --------------------------------- */
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerVelocity = new Vector3(horizontalMove, 0, forwardMove).normalized * sprintSpeed; // sprint
        }
        else
        {
            playerVelocity = new Vector3(horizontalMove, 0, forwardMove).normalized * walkSpeed; // walk
        }
        /* --------------------------------- Output --------------------------------- */
        cc.Move(transform.TransformDirection(playerVelocity) * Time.deltaTime); // move x
        cc.Move(verticalMove * Time.deltaTime); // move y
    }
    private void CreateDebugInfo()
    {
        debugInfo = new MovementDebugParameters
        {
            playerVelocity = playerVelocity,
            verticalMove = verticalMove,
            isGrounded = isGrounded,
        };
    }
}