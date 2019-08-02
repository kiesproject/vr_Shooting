using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VR_Controller : MonoBehaviour
{
    [SerializeField] Transform _transform;

    private SteamVR_Action_Boolean Action_Boolean = SteamVR_Actions._default.GrabPinch;

    private SteamVR_Action_Pose VR_Action_Pose = SteamVR_Actions._default.Pose;

    private Vector3 controller;
    private GameManager GM;
    float hor = 0;
    float ver = 0;
    const float y_top = 90;
    const float y_botton = 15;

    const float x_top = 90;
    const float x_botton = 15;

    Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;

        center = Vector3.zero; //基準点を設定
        //center = transform.localRotation.eulerAngles; //基準点を設定
    
}

    // Update is called once per frame
    void Update()
    {
        if (!GM.VR_Swicth) return;
        
        if (Action_Boolean.GetState(SteamVR_Input_Sources.RightHand))
        {
            Debug.Log("Attak!!");
            GM.Shoot_Trigger = true;

        }
        else
        {
            GM.Shoot_Trigger = false;
        }

        if (Action_Boolean.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            
            Debug.Log("Swich");
            GM.Push_Weapon_Switch();

        }
        

        //controller = (VR_Action_Pose.GetLocalRotation(SteamVR_Input_Sources.RightHand).eulerAngles);
        
        controller = _transform.localRotation.eulerAngles;
        Debug.Log("controller: "+controller);
        Conv_Input(controller,out hor,out ver);
        Debug.Log("hor: " + hor + " | ver: " + ver);
        //GM.Move_key(hor, ver);

    }



    void Conv_Input(Vector3 vector, out float x, out float y)
    {
        x = 0; y = 0;
        Vector3 distance_v3 = vector - center;


        //--- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        float dy = distance_v3.y;
        float dx = distance_v3.x;

        //Debug.Log("d : " + dy);


        if (0 < dy && dy < 180)
        {


            //botton ~ top

            if (y_botton < dy && dy < y_top)
            {
                y = (dy - y_botton) / Mathf.Abs(y_top - y_botton);
            }

            //top~
            if (dy >= y_top)
            {
                y = 1f;
            }
        }
        else if (180 < dy && dy < 360)
        {

            //-top ~ -botton
            if ((360 - y_botton) > dy && dy > (360 - y_top))
            {
                y = -(360 - dy - x_botton) / Mathf.Abs(y_top - y_botton);
            }

            //~-top
            if (dy <= 360 - y_top)
            {
                y = -1f;
            }
        }


        if (0 < dx && dx < 180)
        {
            //botton ~ top

            if (x_botton < dx && dx < x_top)
            {
                x = (dx - x_botton) / Mathf.Abs(x_top - x_botton);
            }

            //top~
            if (dy >= y_top)
            {
                x = 1f;
            }
        }
        else if (180 < dx && dx < 360)
        {

            //-top ~ -botton
            if ((360 - x_botton) > dx && dx > (360 - x_top))
            {
                x = -(360 - dx - x_botton) / Mathf.Abs(x_top - x_botton);
            }

            //~-top
            if (dx <= 360 - x_top)
            {
                x = -1f;
            }
        }

        //--- --- --- --- --- --- --- --- --- --- --- --- --- ---




    }
}





