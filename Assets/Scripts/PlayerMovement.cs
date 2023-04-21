using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] GameObject playerCam;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 7.5f;
    [SerializeField] float lookSpeed = 1f;
    [SerializeField] float maxYLookAngle = 30f;
    [SerializeField] float minYLookAngle = -30f;
    private float yAxis = 0f;
    private float xAxis = 0f;
    Vector3 playerVelocity = Vector3.zero;
    //[SerializeField] float jumpHeight = 1f;
    private CharacterController cc;
    private float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        yAxis += (Input.GetAxis("Mouse Y") * lookSpeed);
        xAxis += (Input.GetAxis("Mouse X") * lookSpeed);
        yAxis = Mathf.Clamp(yAxis, maxYLookAngle * -1, minYLookAngle * -1); // values flipped so that you dont need to provide a - value for max
        gameObject.transform.eulerAngles = new Vector3(yAxis, xAxis, 0);
    }

    private void Move()
    {
        bool isGrounded;
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

        playerVelocity = new Vector3 (horizontalMove,0,forwardMove);
        cc.Move(transform.TransformDirection(playerVelocity) * Time.deltaTime);
        cc.Move(new Vector3(0,verticalMove,0));
    }
}

