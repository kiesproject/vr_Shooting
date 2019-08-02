using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public GameObject Player;
    public List<GameObject> TargetEnemyList;

    [SerializeField]
    public bool VR_Swicth = false;

    #region 変数群

    //Playerのキー入力

    //===x軸の入力状況===
    private float _Horizontal;

    //===y軸の入力状況===
    private float _Vertical;

    //===発射トリガー===
    [HideInInspector]
    public bool Shoot_Trigger = false;

    //===ミサイル発射トリガー===
    [HideInInspector]
    public bool Missile_Trigger = false;
    

    //===切り替えボタン===
    private bool _Weapon_Switch;

    //===カメラ切り替えボタン===
    private bool _Camera_Switch;

    private float Push_Up_Time = 0;

    //===武装状態===
    //0 : 通常装備
    //1 : ミサイル
    //2 : 特殊装備(切り替え不可) 
    private int _Weapon = 0;

    //x軸読み取り専用
    public float Horizontal
    {
        get { return _Horizontal; }
    }

    //y読み取り専用
    public float Vertical
    {
        get { return _Vertical; }
    }

    //発射トリガー読み取り専用
    //public bool Shoot_Trigger
    

    //武器switch
    public bool Weapon_Switch
    {
        get { return _Weapon_Switch; }
    }

    //カメラ切り替え読み取り専用
    public bool Camera_Switch
    {
        get { return _Camera_Switch; }
    }

    //武器装備状況
    public int Weapon
    {
        get { return _Weapon; }
    }

    #endregion

    [HideInInspector] public int downCount = 0; //撃墜した数
    [HideInInspector] public int enemyCounter = 0; //敵の数

    //ゲームの遷移状態
    public int GameState = 0;

    public void Enemy_Count()
    {
        enemyCounter++;
    }

    public void Enemy_Down_Count()
    {
        downCount++;
    }


    //一番最初に実行
    private void Awake()
    {
        //ゲームマネージャーにアクセス出来るようにする。
        if (instance == null)
        { instance = this; }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Trigger_missile: " + Missile_Trigger);
        
        {
            //自動で戻す奴
            Up_Weapon_Switch();
            if (!VR_Swicth)
            {
                var x = Input.GetAxis("Horizontal");
                var y = Input.GetAxis("Vertical");

                Move_key(x, y);
            }

            if (Input.GetKey(KeyCode.V))
            { 
                Shoot_Trigger = true; //ショットを発射
                Debug.Log("発射よ！！");
            }
            else
            {
                Shoot_Trigger = false;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Push_Weapon_Switch();
            }

        }

        Debug.Log(Weapon_Switch);
    }

    //--------------------入力-----------------------

    //武器切り替えボタンを押した
    public void Push_Weapon_Switch()
    {
        this._Weapon_Switch = !_Weapon_Switch;
        if (!Weapon_Switch)
        {
            _Weapon = 0;
        }
        else
        {
            _Weapon = 1;
        }

    }

    //ボタンを押した後自動的に戻す
    private void Up_Weapon_Switch()
    {
        //_Weapon_Switch = false;
        //_Shoot_Trigger = false;
        //Missile_Trigger = false;

    }

    //カメラ切り替えボタンを押しているかどうか
    public void Push_Camera_Switch(bool input)
    {
        if (input == true)
        {
            this._Camera_Switch = true;
        }
        else
        {
            input = false;
        }
    }

    //ショットを撃つ
    public void Push_Trigger()
    {
        this.Shoot_Trigger = true;
    }

    //ミサイルを打つ
    public void Push_Missile()
    {
        this.Missile_Trigger = true;
    }


    //移動の入力状況を設定する。
    public void Move_key(float x, float y)
    {
        this._Horizontal = x;
        this._Vertical = y;
    }

    //武器状況
    public void SetWeapon(int num)
    {
        _Weapon = num;
    }
    
    //敵からの死亡報告
    public void TargetEnemyDead(GameObject o)
    {
        //リストから除く
        if (TargetEnemyList.Contains(o))
            TargetEnemyList.Remove(o);
    }

}
