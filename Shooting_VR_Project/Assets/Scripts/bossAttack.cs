using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : AirFighter
{
    public GameObject bossmuzzle1;
    public GameObject bossmuzzle2;
    public GameObject bossmuzzle3;
    public GameObject bossmuzzle4;
    public GameObject beammuzzle1;
    public GameObject beammuzzle2;
    public GameObject beammuzzle3;
    public GameObject beammuzzle4;
    public GameObject bullet;
    public GameObject beam;
    public GameObject Boss;
    public GameObject explosion_Boss;
    public GameObject explosion_Boss_mini;
    float expltime;
    float timeee;
    float stoptime;
    bool flag = true;
    bool move = true;
    bool down = false;
    public float BossSpeed = 1.0f;
    Vector3[] target = {  new Vector3(0,0,0),
                          new Vector3(5, 0, 0),
                          new Vector3(0, 5, 0),
                          new Vector3(0, 0, 5)};

    public Vector3 first_poss;
    int v, a, c;

    [SerializeField]
    private GameObject Boss_Object;

    public bool boss_stopFlag = false;


    protected override void Start()
    {

        GetComponent<Boss_effect>().enabled = false;

        GameManager.instance.Enemy_Count();
        base.Start();
        max_hp = 500;
        hp = max_hp;

        //first_poss = transform.position;
        v = 0;
        a = 0;
    }

    protected override void Shooting_down(){
        expltime += Time.deltaTime;
        GameManager.instance.Enemy_Down_Count();

        if (expltime >= 0.3f){
            Explosion_mini();
            expltime = 0;
            c += 1;
        }
        
        if (c==10f)
        {
            GameManager.instance.GameState = 2;
            Explosion();
        }
    }

    private void Explosion()
    {
        Instantiate(explosion_Boss, Boss.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void Explosion_mini()
    {
        for (int i=0; i<4; i++)
            Instantiate(explosion_Boss_mini, Boss.transform.position + new Vector3(Random.Range(-7,7), Random.Range(-7, 7), Random.Range(-7, 7)), Quaternion.identity);
    }

    //弾幕攻撃と動き
    private void Danmaku1()
    {
        if (bullet != null) {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle1.transform.position, Quaternion.Euler(bossmuzzle1.transform.localRotation.eulerAngles + new Vector3(0, i * 5 - 50, 0)));
            }
        }

        if (bullet != null)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle1.transform.position, Quaternion.Euler(bossmuzzle1.transform.localRotation.eulerAngles + new Vector3(i * 10 - 90, 0, 0)));
            }
        }

        if (bullet != null)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle2.transform.position, Quaternion.Euler(bossmuzzle2.transform.localRotation.eulerAngles + new Vector3(0, i * 5 + 50, 0)));
            }
        }

        if (bullet != null)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle2.transform.position, Quaternion.Euler(bossmuzzle2.transform.localRotation.eulerAngles + new Vector3(i * 10 - 90, 90, 0)));
            }
        }

        if (bullet != null)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle3.transform.position, Quaternion.Euler(bossmuzzle3.transform.localRotation.eulerAngles + new Vector3(0, i * 5 + 130, 0)));
            }
        }

        if (bullet != null)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle3.transform.position, Quaternion.Euler(bossmuzzle3.transform.localRotation.eulerAngles + new Vector3(i * 10 - 90, 180, 0)));
            }
        }

        if (bullet != null)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle4.transform.position, Quaternion.Euler(bossmuzzle4.transform.localRotation.eulerAngles + new Vector3(0, i * 5 + 230, 0)));
            }
        }

        if (bullet != null)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle4.transform.position, Quaternion.Euler(bossmuzzle4.transform.localRotation.eulerAngles + new Vector3(i * 10 - 90, 270, 0)));
            }
        }
    }

    //弾幕攻撃2と動き
    private void Danmaku2()
    {
        //prefabから配置
        if (bullet != null)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    Instantiate(bullet, bossmuzzle1.transform.position, Quaternion.Euler(bossmuzzle1.transform.localRotation.eulerAngles + new Vector3(10 * j - 10, i * 5 - 50, 0)));
                }

                for (int i = 0; i < 20; i++)
                {
                    Instantiate(bullet, bossmuzzle2.transform.position, Quaternion.Euler(bossmuzzle2.transform.localRotation.eulerAngles + new Vector3(10 * j - 10, i * 5 + 50, 0)));
                }

                for (int i = 0; i < 20; i++)
                {
                    Instantiate(bullet, bossmuzzle3.transform.position, Quaternion.Euler(bossmuzzle3.transform.localRotation.eulerAngles + new Vector3(10 * j - 10, i * 5 + 150, 0)));
                }

                for (int i = 0; i < 20; i++)
                {
                    Instantiate(bullet, bossmuzzle4.transform.position, Quaternion.Euler(bossmuzzle4.transform.localRotation.eulerAngles + new Vector3(10 * j - 10, i * 5 + 230, 0)));
                }
            }
        }
    }

    private void Danmaku3()
    {
        //prefabから配置
        if (bullet != null)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle1.transform.position, Quaternion.Euler(bossmuzzle1.transform.localRotation.eulerAngles + new Vector3(i * 10, i * 10 - 50, 0)));
            }
        }
        if (bullet != null)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle2.transform.position, Quaternion.Euler(bossmuzzle2.transform.localRotation.eulerAngles + new Vector3(i * 10, i * 10 + 50, 0)));
            }
        }

        if (bullet != null)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle3.transform.position, Quaternion.Euler(bossmuzzle3.transform.localRotation.eulerAngles + new Vector3(i * 10, i * 10 + 150, 0)));
            }
        }

        if (bullet != null)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(bullet, bossmuzzle4.transform.position, Quaternion.Euler(bossmuzzle4.transform.localRotation.eulerAngles + new Vector3(i * 10, i * 10 + 230, 0)));
            }
        }
    }

    //ビーム攻撃と動き
    private void Beam()
    {
        //prefabから配置
        Instantiate(beam, beammuzzle1.transform.position, Quaternion.Euler(0, 0, 0));
        Instantiate(beam, beammuzzle2.transform.position, Quaternion.Euler(0, 90, 0));
        Instantiate(beam, beammuzzle3.transform.position, Quaternion.Euler(0, 180, 0));
        Instantiate(beam, beammuzzle4.transform.position, Quaternion.Euler(0, 270, 0));
    }

    protected override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.up * 3);
        //if (!boss_stopFlag) return;

        if (move == true) {
            timeee += Time.deltaTime / 5;
            Debug.Log(first_poss);
            Boss.gameObject.transform.position = Vector3.Lerp(Boss.transform.position, target[v]+first_poss, timeee);
        }else
        {
            stoptime += Time.deltaTime;
        }


        if (Boss.transform.position == target[v] + first_poss)
        {
            timeee = 0;
            v++;
            flag = true;
            move = false;

            if (hp <= max_hp / 2) {
                if (v == 4)
                {
                    v = 0;
                }
            }else{
                if (v == 3)
                {
                    v = 0;
                }
            }
        }

        if (stoptime >= 3.0f && move == false)
        {
            Debug.Log("aaa");
            move = true;
            stoptime = 0;
        }

        if (v == 0 && flag && move == false)
        {
            Danmaku1();
            flag = false;
        }

        if (v == 1 && flag && move == false)
        {
            Danmaku2();
            flag = false;
        }

        if (v == 2 && flag && move == false)
        {
            Danmaku3();
            flag = false;
        }

        if (v == 3 && flag)
        {
            Beam();
            flag = false;
        }
    }

}
