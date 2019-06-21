using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerBase : AirFighter
{
    

    private GameManager GM;
    private float moveForceMultiplier = 20.0f;
    public Rigidbody rigidbody { private set; get; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GM = GameManager.instance;
        property |= Property.isInvulnerable; //不死身にする
        Launch_AriFighter();

        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
            rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;

    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //移動のメソッド
        //Move_Player();

    }

    //デバッグのみ使用可能
    public void SetSpeed_forDebug(float a)
    {
        airFighter_speed = a;
    }

    //操作移動するメソッド
    void Move_Player()
    {
        transform.position += transform.forward * 0.1f;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerBase))]
    public class PlayerBase_Inspector : AirFighter_Inspector { }


#endif

}



