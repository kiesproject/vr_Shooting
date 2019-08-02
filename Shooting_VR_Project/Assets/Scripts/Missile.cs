using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField, Tooltip("弾速")]
    float speed = 0.1f;
    float dmpSpeed = 0.01f;

    //衝突するレイヤー
    //基本playerかenemyを選択する。
    [SerializeField, Tooltip("当たる物を指定する")]
    private LayerMask layer = 0;

    [SerializeField, Tooltip("ダメージ量")]
    private float damege = 8;

    [SerializeField, Tooltip("爆発のオブジェクト")]
    private GameObject explosion;

    [SerializeField, Tooltip("ターゲットのオブジェクト")]
    private GameObject target;

    private Rigidbody rig;
    private float time = 0;

    [SerializeField]
    private bool trigger = false; //ミサイルを発射したかどうか

    [SerializeField]
    bool ally = false;

    [SerializeField]
    bool enemy = false;

    //---<<TARGET>>---
    private Vector3 poss1;
    private Vector3 poss2;
    private Vector3 poss3;
    private Vector3 poss4;
    //----------------


    // Start is called before the first frame update
    void Start()
    {
        poss1 = transform.position;
        //Shoot(target); //デバッグ用
    }

    private void FixedUpdate()
    {
        //return;
        if (!trigger) return;
        //---------------------------------------------------
        time += Time.deltaTime * speed;
        speed += dmpSpeed;
        poss4 = target.transform.position;
        transform.position = GetPoint(poss1, poss2, poss3, poss4 , time);
        transform.LookAt(GetPoint(poss1, poss2, poss3, poss4, time + 1));

        if (time > 1)
        {
            if (target.GetComponent<AirFighter>() != null)
            {
                AirFighter fighter = target.gameObject.GetComponent<AirFighter>();
                    //ダメージを与える
                    fighter.Damage(damege);
            }
            Explosion();
        }

    }

    //LayerMaskに対象のLayerが含まれているかチェックする
    private bool CompareLayer(LayerMask layerMask, int layer)
    { return ((1 << layer) & layerMask) != 0; }

    //ミサイルを目標に発射する。
    public void Shoot(GameObject gameObject)
    {
        target = gameObject; //目標に登録
        float ran = Random.Range(140, 280);
        poss2 = GetPoss2(transform, ran);
        poss3 = GetPoss3(target.transform.position, poss1);
        trigger = true;
    }

    //ベジェ曲線
    Vector3 GetPoint(Vector3 poss1, Vector3 poss2, Vector3 poss3 , Vector3 poss4 , float t)
    {
        var a = Vector3.Lerp(poss1, poss2, t);
        var b = Vector3.Lerp(poss2, poss3, t);
        var c = Vector3.Lerp(poss3, poss4, t);

        var d = Vector3.Lerp(a, b, t);
        var e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);
        
    }

    //第二地点の計算
    Vector3 GetPoss2(Transform transform ,float l)
    {
        Vector3 vector = new Vector3(Random.Range(-4,4), Random.Range(-4, 4), Random.Range(-4, 4));
        return transform.forward * l + this.transform.position + vector;
    }

    Vector3 GetPoss3(Vector3 target, Vector3 start)
    {
        Vector3 vector = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        return (target + start) / 2 + vector;
    }

    //爆発
    private void Explosion()
    {
        if (explosion != null)
        { Instantiate(explosion, this.transform.position, this.transform.rotation); }
        Destroy(this.gameObject);
    }
}
