using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCam : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] float RotateSpeed;

    float yaw, pitch;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Target.position.x, Target.position.y, Target.position.z);

        yaw += Input.GetAxis("Mouse X") * RotateSpeed * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * RotateSpeed * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -80, 60);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}