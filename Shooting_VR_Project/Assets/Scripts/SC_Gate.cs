using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

#endif

public class SC_Gate : MonoBehaviour
{
    SeaneController SC;
    bool flag = false;

    [SerializeField, TextArea(3, 5)]
    private string memo;

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

#if UNITY_EDITOR
    [DrawGizmo(GizmoType.Active | GizmoType.NonSelected)]
    static void DrawGateGizmos(SC_Gate sC_Gate, GizmoType gizmoType)
    {
        Gizmos.color = new Color(0.8f, 0, 0.8f);
        Gizmos.DrawWireCube(sC_Gate.transform.position, sC_Gate.transform.localScale);
    }
#endif

}
