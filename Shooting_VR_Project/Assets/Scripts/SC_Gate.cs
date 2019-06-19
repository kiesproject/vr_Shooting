using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Gate : MonoBehaviour
{
    SeaneController SC;
    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        SC = SeaneController.sceanController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (flag) return;

        if (other.gameObject.GetComponent<Player>() != null)
        {
            SC.SwitchScean();
            flag = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 1f, 0);
        Gizmos.DrawSphere(transform.position, 1f);
        //Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
