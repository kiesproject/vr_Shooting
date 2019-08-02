using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] Transform _transform;

    private SteamVR_Action_Boolean Action_Boolean = SteamVR_Actions._default.GrabPinch;

    private SteamVR_Action_Pose VR_Action_Pose = SteamVR_Actions._default.Pose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Action_Boolean.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            Debug.Log("Swich");
        }

        if (Action_Boolean.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Debug.Log("Attak!!");
        }

        //Debug.Log(VR_Action_Pose.GetLocalRotation(SteamVR_Input_Sources.RightHand).eulerAngles);

        _transform.eulerAngles = VR_Action_Pose.GetLocalRotation(SteamVR_Input_Sources.RightHand).eulerAngles;

    }
}
