using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public　static GameManager instance;
    public GameObject Player;
    public List<GameObject> TargetEnemyList; 

    #region 変数群

    //Playerのキー入力

    //===x軸の入力状況===
    private float _Horizontal;

    //===y軸の入力状況===
    private float _Vertical;

    //===発射トリガー===
    private bool _Shoot_Trigger;

    //===切り替えボタン===
    private bool _Weapon_Switch;

    //===カメラ切り替えボタン===
    private bool _Camera_Switch;

    //===武装状態===
    //0 : 通常装備
    //1 : ミサイル
    //2 : 特殊装備(切り替え不可) 
    private int _Weapon;

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
    public bool Shoot_Trigger
    {
        get { return _Shoot_Trigger; }
    }

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

    //一番最初に実行
    private void Awake()
    {
        //ゲームマネージャーにアクセス出来るようにする。
        if (instance == null)
        { instance = this; }
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        Move_key(x, y);


    }

    //--------------------入力-----------------------

    //武器切り替えボタンを押した
    public void Push_Weapon_Switch()
    {
        this._Weapon_Switch = true;
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
        this._Shoot_Trigger = true;
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
}
