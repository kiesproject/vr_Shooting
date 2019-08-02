using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Controller_test : MonoBehaviour
{
    Vector3 vector;

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
        center = transform.localRotation.eulerAngles; //基準点を設定
    }

    // Update is called once per frame
    void Update()
    {
        //角度を取得
        vector = transform.localRotation.eulerAngles;
        Debug.Log("vector: "+vector);
        Conv_Input(vector, out hor, out ver);
        Debug.Log("x: " + hor + "  y: " + ver);

    }

    void Conv_Input(Vector3 vector, out float x, out float y)
    {
        x = 0;
        y = 0;



    }
}
