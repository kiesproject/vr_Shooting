using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Bullet : Bullet
{
    private float missile_timer = 0; //飛行している時間
    private float mtime = 0;

    private float overtime = 0.5f; //直進する時間
    private bool chaserFlag = false; //
    private float addspeed = 0;

    [SerializeField, Tooltip("ターゲットのオブジェクト")]
    private GameObject target;

    //-----<<TARGET>>-----
    private Vector3 poss1;
    private Vector3 poss2;
    private Vector3 poss3;
    private Vector3 poss4;
    //--------------------

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update(){
        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (target == null) return;
        Debug.Log(target);

        if(missile_timer <= overtime ) //直進
        {
            addspeed += 0.001f;
            missile_timer += Time.deltaTime * 0.1f + addspeed;//タイマー

            transform.position += transform.forward; //直進
            return;
        }
        else //追跡
        {
            if(mtime==0)
                ChaseStart_Target(); //追跡のための設定

            
            try {
                transform.LookAt(GetPoint(poss1, poss2, poss3, target.transform.position, mtime + 0.0001f));
                transform.position = GetPoint(poss1, poss2, poss3, target.transform.position, mtime);
            } catch {
                target = null;
                Explosion();
            }
            mtime += Time.deltaTime;
            if (mtime >= 1) //ターゲットまでたどり着いたらダメージを与える。
            {
                Damage();
            }

        }
        
    }

    //追跡処理の初期設定
    void ChaseStart_Target()
    {
        if (chaserFlag) return;
        poss1 = transform.position;
        poss2 = GetPoss2(10);
        poss3 = GetPoss3(transform.position, target.transform.position);
        poss4 = target.transform.position;
        //missile_timer = 0;

        chaserFlag = true;
    }

    

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    //第二地点の計算
    Vector3 GetPoss2(float l)
    {
        Vector3 vector = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), Random.Range(-4, 4));
        return transform.forward * l + transform.position + vector ;
    }

    Vector3 GetPoss3(Vector3 target, Vector3 start)
    {
        Vector3 vector = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        return (target + start) / 2 + vector ;
    }

    //ベジェ曲線
    Vector3 GetPoint(Vector3 poss1, Vector3 poss2, Vector3 poss3, Vector3 poss4, float t)
    {
        var a = Vector3.Lerp(poss1, poss2, t);
        var b = Vector3.Lerp(poss2, poss3, t);
        var c = Vector3.Lerp(poss3, poss4, t);

        var d = Vector3.Lerp(a, b, t);
        var e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);

    }

    //衝突判定は呼び出さないといけない？
    void Damage()
    {
        var air = target.GetComponent<AirFighter>();
        Debug.Log("ダメージ(missile)");
        //ダメージを与える
        air.Damage(damege);
        Explosion();
    }

    private void OnDestroy()
    {
        target = null;
    }


}
