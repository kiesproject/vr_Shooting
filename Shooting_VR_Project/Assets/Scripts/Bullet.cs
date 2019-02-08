using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Bullet : MonoBehaviour
{
    [SerializeField, Tooltip("弾速")]
    float speed = 10;

    //衝突するレイヤー
    //基本playerかenemyを選択する。
    [SerializeField, Tooltip("当たる物を指定する")]
    private LayerMask layer = 0;

    [SerializeField, Tooltip("消滅時間")]
    private float timer = 5;

    [SerializeField, Tooltip("ダメージ量")]
    private float damege = 2;

    [SerializeField, Tooltip("爆発のオブジェクト")]
    private GameObject explosion;

    private Rigidbody rig;
    private float time = 0;

    [SerializeField]
    bool ally = false;
    [SerializeField]
    bool enemy = false;

    // Start is called before the first frame update
    void Start()
    {
        //rigidbodyを取得する。
        rig = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //時間の管理
        TimeKeeper(); ;

    }

    private void FixedUpdate()
    {
        //進むだけ
        Straight();
    }

    //まっすぐ進む(update)
    private void Straight()
    {
        rig.velocity = transform.forward * speed;
        //transform.position += transform.forward * speed;
    }

    //弾消滅
    private void TimeKeeper()
    {
        time += Time.deltaTime;

        if (time > timer)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision c)
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


        }
        Explosion();
    }

    private void Explosion()
    {
        if (explosion != null)
        { Instantiate(explosion, this.transform.position, this.transform.rotation); }
        Destroy(this.gameObject);
    }


    //LayerMaskに対象のLayerが含まれているかチェックする
    private bool CompareLayer(LayerMask layerMask, int layer)
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
            //base.OnInspectorGUI();
            
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