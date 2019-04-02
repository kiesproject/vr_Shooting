using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour {

    public float mouseSpeed = 1;
    public GameObject Haed;
    CharacterController characterController;

    public float MoveSpeed = 10.0f;
    public float JumpSpeed = 20.0f;
    public float Gravity = 9.8f;

    Vector3 MoveVector = Vector3.zero;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
   
        characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        this.transform.Rotate(Vector3.up, x * mouseSpeed * Time.deltaTime,Space.World);
        Haed.transform.Rotate(this.transform.right, -y * mouseSpeed * Time.deltaTime, Space.World);

        float Move_x = Input.GetAxis("Horizontal");
        float Move_z = Input.GetAxis("Vertical");


        if (characterController.isGrounded) {
            if (Move_x != 0)
            {

                MoveVector.x = Move_x * MoveSpeed;

            }
            else
            {

                MoveVector.x = 0;

            }

            if (Move_z != 0)
            {

                MoveVector.z = Move_z * MoveSpeed;

            }
            else
            {

                MoveVector.z = 0;

            }

            if (Input.GetButtonDown("Jump")) {
                MoveVector.y = JumpSpeed;
            }

        }

        MoveVector.y -= Gravity * Time.deltaTime;

        characterController.Move( transform.TransformDirection(MoveVector * Time.deltaTime) );

    }
}
