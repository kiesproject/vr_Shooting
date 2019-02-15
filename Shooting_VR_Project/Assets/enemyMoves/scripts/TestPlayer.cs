using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    CharacterController characterController;

    public float MoveSpeed = 10.0f;
    public float JumpSpeed = 20.0f;

    Vector3 MoveVector = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float Move_x = Input.GetAxis("Horizontal");
        float Move_z = Input.GetAxis("Vertical");

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

        if (Input.GetButton("Jump"))
        {
            MoveVector.y = JumpSpeed;
        }
        else if(Input.GetButton("Fire3"))
        {
            MoveVector.y = -JumpSpeed;
        }
        else
        {
            MoveVector.y = 0;
        }

        characterController.Move(transform.TransformDirection(MoveVector * Time.deltaTime));

    }
}
