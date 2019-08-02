using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR;

public class LoadingSystem : MonoBehaviour
{

    private SteamVR_Action_Boolean Action_Boolean = SteamVR_Actions._default.GrabPinch;
    public Image startbutton;
    public Text ltext;

    // Start is called before the first frame update
    void Start()
    {

        startbutton = GameObject.Find("StartButton").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Action_Boolean.GetState(SteamVR_Input_Sources.RightHand) || Action_Boolean.GetState(SteamVR_Input_Sources.LeftHand) || Input.GetKey(KeyCode.S))
        {
            startbutton.fillAmount += 0.01f;
        }
        else
        {
            startbutton.fillAmount = 0;
        }
        if(startbutton.fillAmount > 0.99)
        {
            GameManager.instance.GameState = 1;
            SceneManager.LoadScene("OP");
            //SceneManager.LoadSceneAsync("wave0");
            //SceneManager.LoadSceneAsync("wave1");
            //SceneManager.LoadSceneAsync("wave2");
            //SceneManager.LoadSceneAsync("wave3");
            //SceneManager.LoadSceneAsync("wave4");
            //SceneManager.LoadSceneAsync("wave5");
            //SceneManager.LoadSceneAsync("wave6");
        }
    }
}
