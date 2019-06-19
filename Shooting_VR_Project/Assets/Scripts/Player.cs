using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AirFighter
{
    GameManager GM;

    [SerializeField]
    GameObject bullet_N; //標準装備の弾丸

    [SerializeField]
    GameObject muzzle;

    float time = 0;
    bool shootNegativeFlag = false;

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
    }

    //ショットの受付
    void Input_Shoot()
    {
        if (GM.Shoot_Trigger && !shootNegativeFlag)
        {
            switch (GM.Weapon)
            {
                case 0:
                    Normal_Shoot();
                    shootNegativeFlag = true;
                    break;
                case 1:

                    break;
                default:
                    break;
            }
        }
        
        if (shootNegativeFlag)
        {
            time += Time.deltaTime;
            if (time > 0.3f)
            {
                time = 0;
                shootNegativeFlag = false;
            }
        }

    }

    //通常のショット発射する
    void Normal_Shoot()
    {
        if (bullet_N == null) return;
        if (muzzle == null) return;
        GameObject bullet = Instantiate(bullet_N, muzzle.transform.position, muzzle.transform.rotation) as GameObject;
    }

}
