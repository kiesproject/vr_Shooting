using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLuncher : MonoBehaviour
{
    public GameObject Bomb;
    public float shotSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
    }

    void Shot()
    {
        GameObject @object = (GameObject)Instantiate(
                Bomb,
                this.transform.position,
                Quaternion.identity
            );

        Rigidbody objectRigitbody = @object.GetComponent<Rigidbody>();
        objectRigitbody.AddForce(this.transform.forward * shotSpeed);
    }
}
