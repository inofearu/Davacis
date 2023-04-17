using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5f;
    //[SerializeField] float sprintSpeed = 7.5f;
    //[SerializeField] float jumpHeight = 1f;
    [SerializeField] float rotationSpeed = 1f;
    private CharacterController cc;
    private bool isGrounded;
    private float gravity = -9.81f;
    private Vector3 playerVelocity;
    private float playerSpeed;
    private Vector3 cameraRotation;
    private Vector3 cameraTargetRotation;
    [SerializeField] GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        isGrounded = cc.isGrounded;
        if (isGrounded && cc.velocity.y < 0.1)
        {
            playerVelocity.y = 0f;
        }
        playerVelocity = new Vector3(Input.GetAxisRaw("Horizontal") * walkSpeed,(playerVelocity.y + (gravity * Time.deltaTime)),Input.GetAxisRaw("Forward") * walkSpeed);
        cc.Move(playerVelocity * Time.deltaTime);
        MoveCamera();
    }
    private void MoveCamera()
    {
        //cameraRotation = new Quaternion.eulerAngles(camera.transform.rotation.x,camera.transform.rotation.y,camera.transform.rotation.z);
        //rotationSpeed),cameraRotation[1] + (Input.GetAxis("Mouse X") * rotationSpeed), cameraRotation[2]);
        cameraRotation = camera.transform.localRotation.eulerAngles;
       // cameraTargetRotation = new Vector3(cameraRotation.x + (Input.GetAxis("Mouse X") * rotationSpeed),cameraRotation.y + (Input.GetAxis("Mouse Y") * rotationSpeed),cameraRotation.z);
       // camera.transform.Rotate(cameraTargetRotation);
        Debug.Log($"{cameraRotation}");
    }
}

