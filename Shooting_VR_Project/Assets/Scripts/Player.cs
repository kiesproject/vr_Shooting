using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Player : AirFighter
{
    GameManager GM;

    [SerializeField]
    GameObject bullet_N; //標準装備の弾丸

    [SerializeField]
    GameObject muzzle1;
    [SerializeField]
    GameObject muzzle2;

    float time = 0;
    bool shootNegativeFlag = false;

    float debuffTime = 0; //バフに使用するタイマー
    bool isDebuff = false;
    float debuff_desmove_Per = 1;
    float debuff_desshoot_Per = 1;

    private SteamVR_Action_Boolean Action_Boolean = SteamVR_Actions._default.GrabPinch;

    private void Awake()
    {
        GM = GameManager.instance;
        GM.Player = this.gameObject;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        max_hp = 40;
        hp = max_hp;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Input_Shoot();
        Chack_Debuff();
    }

    //ショットの受付
    void Input_Shoot()
    {


        Debug.Log("GM.Shoot_Trigger: " + GM.Shoot_Trigger);
        //Debug.Log("shootNegativeFlag: " + shootNegativeFlag);
        if (GM.Shoot_Trigger  && !shootNegativeFlag)
        {
            switch (GM.Weapon)
            {
                case 0:
                    Normal_Shoot();
                    shootNegativeFlag = true;
                    break;
                case 1:
                    GM.Push_Missile();
                    shootNegativeFlag = true;
                    //GameManager.instance.Missile_Trigger = false;

                    break;
                default:
                    break;
            }
        }
        else
        {
            GameManager.instance.Missile_Trigger = false;
        }
        
        if (shootNegativeFlag)
        {
            time += Time.deltaTime;
            if (time > 0.3f * debuff_desshoot_Per)
            {
                time = 0;
                shootNegativeFlag = false;
            }
        }

    }

    //通常のショット発射する
    private void Normal_Shoot()
    {
        if (bullet_N == null) return;
        if (muzzle1 == null) return;

        GameObject bullet1 = Instantiate(bullet_N, muzzle1.transform.position, muzzle1.transform.rotation) as GameObject;
        GameObject bullet2 = Instantiate(bullet_N, muzzle2.transform.position, muzzle1.transform.rotation) as GameObject;

    }

    private void Chack_Debuff() //デバフの処理を行う
    {
        if (isDebuff)
        {
            debuffTime += Time.deltaTime;
            debuff_desmove_Per = 0.8f; //移動割合
            debuff_desshoot_Per = 0.5f; //ショット感覚割合

            if (debuffTime >= 3f)
            {
                isDebuff = false;
            }
        }
        else
        {
            debuffTime = 0;
            debuff_desmove_Per = 1;
            debuff_desshoot_Per = 1;
        }

    }

    public void Set_Debuff()
    {
        isDebuff = true;
        debuffTime = 0;
    }

}
