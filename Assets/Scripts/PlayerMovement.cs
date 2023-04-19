using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 playerVelocity = Vector3.zero;
    [SerializeField] float walkSpeed = 5f;
    float sprintSpeed = 7.5f;
    //[SerializeField] float jumpHeight = 1f;
    private CharacterController cc;
    private float playerSpeed;
    [SerializeField] GameObject playerCam;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        bool isGrounded;
        bool isSprinting = false;
        float moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
        isGrounded = cc.isGrounded;

        float horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float forwardMove = Input.GetAxisRaw("Forward") * moveSpeed;
        float verticalMove = playerVelocity.y + -9.81f * Time.deltaTime;

        if (isGrounded && cc.velocity.y < 0.1)
        {
            verticalMove = 0f;
        }

        playerVelocity = new Vector3 (horizontalMove,verticalMove,forwardMove);
        cc.Move(transform.TransformDirection(playerVelocity) * Time.deltaTime);
    }
}

