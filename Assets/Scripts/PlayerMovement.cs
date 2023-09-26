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
    /* --------------------------------- Objects -------------------------------- */
    [SerializeField] private GameObject playerEyes;
    private CharacterController cc;
    /* -------------------------------- Movement -------------------------------- */
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float groundedTolerance = 0.11f; // dist that spherecast extends below player
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float sphereCastSize = 0.5f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    private Vector3 playerVelocity = Vector3.zero;
    private Vector3 verticalMove = Vector3.zero;
    /* --------------------------------- Camera --------------------------------- */
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float maxYLookAngle = 50f;
    [SerializeField] private float minYLookAngle = -75f;
    private float yAxis = 0f; // affected by player mouse
    private float xAxis = 0f; // affected by player mouse
    /* ---------------------------------- Debug --------------------------------- */
    [SerializeField] private bool showSpherecastDebug = false;

    [UsedImplicitly]
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // captures cursor
    }
    [UsedImplicitly]
    private void Update()
    {
        Rotate();
        Move();
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
        bool isGrounded;
        /* ---------------------------------- Input --------------------------------- */
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float forwardMove = Input.GetAxisRaw("Forward");
        verticalMove += new Vector3(0, playerVelocity.y + (gravity * Time.deltaTime), 0);
        /* ------------------------------- SphereCast ------------------------------- */
        Physics.SphereCast(transform.position - new Vector3(0, 0.5f, 0), sphereCastSize, transform.up * -1f, out RaycastHit hitData); // ground 
        /* ------------------------------ Ground Check ------------------------------ */
        if (hitData.distance == 0f) // contingency incase player is above void as sphereCast misses
        {
            isGrounded = false;
        }
        else if (hitData.distance <= groundedTolerance)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        /* ---------------------------------- Jump ---------------------------------- */
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalMove += new Vector3(0, jumpHeight, 0);
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
        // Debug.Log($"{playerVelocity} - {verticalMove} - {hitData.distance} - {isGrounded}");
        cc.Move(transform.TransformDirection(playerVelocity) * Time.deltaTime); // X move
        cc.Move(verticalMove * Time.deltaTime); // Y move
    }
    [UsedImplicitly]
    private void OnDrawGizmos()
    {
        /* --------------------------- SphereCastDebugDraw -------------------------- */
        if (showSpherecastDebug) // TODO: Implement in-game toggle, use prefab method
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, sphereCastSize);
        }
    }
}