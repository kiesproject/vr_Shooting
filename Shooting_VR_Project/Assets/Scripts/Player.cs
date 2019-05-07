using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AirFighter
{
    private Vector3 front;
    private Vector3 accev;

    private GameManager GM;
    private float horizontal;
    private float vertical;

    [SerializeField]
    private float speed = 1.0f;

    private Rigidbody rig;
    private float moveForceMultiplier = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //キー入力
        horizontal = GM.Horizontal;
        vertical = GM.Vertical;
        Debug.Log("[Player] hor:" + horizontal);
        Debug.Log("[Player] ver:" + vertical);
    }

    private void FixedUpdate()
    {
        //移動のメソッド
        Move_Player_Meta();
        //transform.position += new Vector3(0, 0, 0.1f);
    }

    //
    void DirectionSet()
    {

    }

    //操作移動するメソッド
    void Move_Player_Meta()
    {
        //入力情報をリセット
        Vector3 moveVector = Vector3.zero;

        //入力情報を設定
        moveVector.x = speed * horizontal;
        moveVector.y = speed * vertical;

        //入力ベクトル - 現在の速度ベクトル・移動
        rig.AddForce(moveForceMultiplier * (moveVector - rig.velocity));
    }


    


}
