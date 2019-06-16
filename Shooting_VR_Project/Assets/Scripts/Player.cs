﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private GameManager GM;
    private float horizontal;
    private float vertical;
    private PlayerBase playerBase;

    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private GameObject PlayerModel;

    private Vector3 headVector;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;
        playerBase = transform.parent.gameObject.GetComponent<PlayerBase>(); //PlayerBaseを取得 
    }

    // Update is called once per frame
    void Update()
    {
        //キー入力
        horizontal = GM.Horizontal;
        vertical = GM.Vertical;
    }

    void FixedUpdate()
    {
        //移動のメソッド
        Move_Player_Meta();
        //RotateHead();
    }

    //操作移動するメソッド
    void Move_Player_Meta()
    {
        //入力情報をリセット
        Vector3 moveVector = Vector3.zero;
        float dx, dy;
        LimitMove(out dx, out dy);
        //dx = 0; dy = 0;
        Debug.Log("dx:"+dx+" dy:"+dy);
        //dy = 0;

        //入力情報を設定
        moveVector = (Vector3.right - (dx * Vector3.right)) * speed * horizontal + (Vector3.up - (dy * Vector3.up)) * speed * vertical;

        Debug.Log("moveVector:" + moveVector);

        //移動する
        transform.localPosition += moveVector * Time.deltaTime;
        PlayerModel.transform.localPosition = transform.localPosition  -0.1f* Vector3.forward - 0.02f * moveVector;
        PlayerModel.transform.LookAt(transform.position);
    }

    //本体を回す
    void RotateHead()
    {
        float m = 80.0f;
        float d = 5.0f;
        //方向ベクトルを設定(ゆっくりもどる)
        headVector = Vector3.right * (horizontal - headVector.x) / d + Vector3.up * (vertical - headVector.y) / d;
        Debug.Log("headVector:" + headVector);

        //プレイヤーモデルを動かす
    }

    //動ける範囲を制限する
    void LimitMove(out float dx, out float dy)
    {
        float limit = 4.0f; //矩形半径
        float a = 0.5f; 

        if ((limit - a <= transform.localPosition.x) && (horizontal > 0))
        {
            float distance = Mathf.Abs((limit - a) - transform.localPosition.x) * (1.0f / a);
            Debug.Log("distance.x:" + distance);
            if (distance > 1)
            {
                dx = 1;
            }
            else
            {
                dx = distance;
            }
        }
        else if ((transform.localPosition.x <= -limit + a) && (horizontal < 0))
        {

            float distance = Mathf.Abs((-limit + a) - transform.localPosition.x)* (1.0f / a);
            Debug.Log("distance.x:" + distance);
            if (distance > 1)
            {
                dx = 1;
            }
            else
            {
                dx = distance;
            }
        }
        else
        {
            dx = 0;
        }

        Debug.Log("y: " + transform.localPosition.y);

        if ((limit - a <= transform.localPosition.y) && (vertical > 0))
        {
            float distance = Mathf.Abs((limit - a) - transform.localPosition.y) * (1.0f / a);
            Debug.Log("distance.y:" + distance);
            if (distance > 1)
            {
                dy = 1;
            }
            else
            {
                dy = distance;
            }
        }
        else if ((transform.localPosition.y <= -limit + a) && (vertical < 0))
        {

            float distance = Mathf.Abs((-limit + a) - transform.localPosition.y) * (1.0f / a);
            Debug.Log("distance.y:" + distance);
            if (distance > 1)
            {
                dy = 1;
            }
            else
            {
                dy = distance;
            }
        }
        else
        {
            dy = 0;
        }
    }
}
