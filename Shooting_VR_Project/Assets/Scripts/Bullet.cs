using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Bullet : MonoBehaviour
{
    [SerializeField, Tooltip("弾速")]
    protected float speed = 10;

    //衝突するレイヤー
    //基本playerかenemyを選択する。
    [SerializeField, Tooltip("当たる物を指定する")]
    protected LayerMask layer = 0;

    [SerializeField, Tooltip("消滅時間")]
    protected float timer = 5;

    [SerializeField, Tooltip("ダメージ量")]
    protected float damege = 2;

    [SerializeField, Tooltip("爆発のオブジェクト")]
    protected GameObject explosion;

    protected Rigidbody rig;
    private float time = 0;

    [SerializeField]
    protected bool ally = false;
    [SerializeField]
    protected bool enemy = false;

    [SerializeField]
    float picth = 1;
    [SerializeField]
    string fileName = "enemy_shoot";

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //rigidbodyを取得する。
        rig = GetComponent<Rigidbody>();
        SoundManager.instance.Instance_Sound(transform.position, fileName,picth);

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //時間の管理
        TimeKeeper(); ;

    }

    protected virtual void FixedUpdate()
    {
        //進むだけ
        Straight();
    }

    //まっすぐ進む(update)
    protected virtual void Straight()
    {
        rig.velocity = transform.forward * speed;
        //transform.position += transform.forward * speed;
    }

    //弾消滅
    protected void TimeKeeper()
    {
        time += Time.deltaTime;

        if (time > timer)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision c)
    {
        //当たった物が戦闘機
        if (c.gameObject.GetComponent<AirFighter>() != null)
        {
            AirFighter fighter = c.gameObject.GetComponent<AirFighter>();

            if (CompareLayer(layer, c.gameObject.layer))
            {
                Debug.Log("ダメージ");
                //ダメージを与える
                fighter.Damage(damege);
            }

            Explosion();
        }
    }

    protected void Explosion()
    {
        if (explosion != null)
        { Instantiate(explosion, this.transform.position, this.transform.rotation); }
        Destroy(this.gameObject);
    }


    //LayerMaskに対象のLayerが含まれているかチェックする
    protected bool CompareLayer(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }



    //inspector拡張
#if UNITY_EDITOR
    [CustomEditor(typeof(Bullet))]
    public class Bullet_Editor : Editor
    {
        bool folding = false;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            //上のクラスを取得
            Bullet bullet = target as Bullet;

            EditorGUILayout.LabelField("=====【弾丸の設定】=====");
            EditorGUILayout.Space();

            bullet.speed = EditorGUILayout.FloatField("弾丸の速さ", bullet.speed);
            bullet.damege = EditorGUILayout.FloatField("ダメージ値", bullet.damege);
            bullet.timer = EditorGUILayout.FloatField("アクティブ時間", bullet.timer);
            bullet.explosion = EditorGUILayout.ObjectField("爆発のPrefab",bullet.explosion, typeof(GameObject), true) as GameObject;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("【弾丸の有効機】");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("味方機", GUILayout.Width(48));
            bullet.ally = EditorGUILayout.Toggle(bullet.ally, GUILayout.Width(48));
            //ally = EditorGUILayout.Toggle("味方機", ally, GUILayout.Width(45));

            EditorGUILayout.LabelField("敵機", GUILayout.Width(48));
            bullet.enemy = EditorGUILayout.Toggle(bullet.enemy, GUILayout.Width(48));
            //enemy = EditorGUILayout.Toggle("敵機", enemy, GUILayout.ExpandHeight(false));
            EditorGUILayout.EndHorizontal();

            if (bullet.ally)
            { bullet.layer = bullet.layer | (1 << LayerMask.NameToLayer("Player")); }
            else
            { bullet.layer = bullet.layer & ~(1 << LayerMask.NameToLayer("Player")); }

            if (bullet.enemy)
            { bullet.layer = bullet.layer | (1 << LayerMask.NameToLayer("Enemy")); }
            else
            {
                bullet.layer = bullet.layer & ~(1 << LayerMask.NameToLayer("Enemy"));
            }



            }
    }
#endif

}