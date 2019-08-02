using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Buff_Enemy : AirFighter
{
    float shootTime = 0;

    [SerializeField]
    private GameObject muzzle;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject bullet;

    private GameObject player;

    private Vector3 target_1_v3;
    private Vector3 target_2_v3;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        max_hp = 8;
        hp = max_hp;

        GameManager.instance.Enemy_Count();
        player = GameManager.instance.Player;
        Launch_AriFighter();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();



        if (Distance_Player() < 15)
        {
            if (shootTime == 0)
            {
                target_1_v3 = player.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                target_2_v3 = muzzle.transform.forward * Distance_Player();
            }


            if (shootTime > 1.5)
            {
                shootTime = 0;
                muzzle.transform.LookAt(player.transform.position + new Vector3(
                    Random.Range(-0.2f, 0.2f),
                    Random.Range(-0.2f, 0.2f),
                    Random.Range(-0.2f, 0.2f)));

                Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
                return;
            }

            //muzzle.transform.LookAt(Vector3.Lerp(target_2_v3, target_1_v3, shootTime));
            shootTime += Time.deltaTime;
        }
        /*
        var v = player.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        muzzle.transform.LookAt(v);
        */
    }

    protected override void Shooting_down()
    {
        base.Shooting_down();
        GameManager.instance.Enemy_Down_Count();
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);

    }

    void Shoot()
    {
        if (Distance_Player() < 15)
        {
            shootTime += Time.deltaTime;
            if (shootTime > 1)
            {
                shootTime = 0;
                Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
            }
        }

        var v = player.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        muzzle.transform.LookAt(v);
    }

    float Distance_Player() //プレイヤーとの距離
    { return Vector3.Distance(transform.position, player.transform.position); }


#if UNITY_EDITOR
    [CustomEditor(typeof(Buff_Enemy))]
    public class Buff_Enemy_Inspector : AirFighter_Inspector
    {
        public override void SetComponent()
        {
            base.SetComponent();
            component = target as Buff_Enemy;
        }
    }

    public class EditorGizmo_Buff_Enemy : EditorGizmo_AirFighter { }

#endif
}

