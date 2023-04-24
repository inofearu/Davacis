using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] GameObject playerEyes;
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 7.5f;
    [SerializeField] float lookSpeed = 1f;
    [SerializeField] float maxYLookAngle = 30f;
    [SerializeField] float minYLookAngle = -30f;
    Vector3 verticalMove = Vector3.zero;
    private float yAxis = 0f;
    private float xAxis = 0f;
    Vector3 playerVelocity = Vector3.zero;
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

        gameObject.transform.eulerAngles = new Vector3(0, xAxis, 0);
        playerEyes.transform.eulerAngles = new Vector3(yAxis,xAxis,0);
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float forwardMove = Input.GetAxisRaw("Forward");
        verticalMove += new Vector3 (0,playerVelocity.y + -9.81f * Time.deltaTime,0);
        Debug.Log(verticalMove);
        if(cc.isGrounded)
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

        if(Input.GetKey(KeyCode.LeftShift))
        {
            playerVelocity = new Vector3 (horizontalMove,0,forwardMove).normalized * sprintSpeed;
        }
        else
        {
            playerVelocity = new Vector3 (horizontalMove,0,forwardMove).normalized * walkSpeed;
        }

        cc.Move(transform.TransformDirection(playerVelocity) * Time.deltaTime);
        cc.Move(verticalMove * Time.deltaTime);
    }
}