using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileManager : MonoBehaviour
{
    static public missileManager instance;

    [SerializeField]
    private GameObject[] missiles_pack;

    [SerializeField]
    private GameObject missile;

    [SerializeField]
    private int VerWidth = 5;
    [SerializeField]
    private int Width = 6;
    [SerializeField]
    private float Interval = 5;

    private List<GameObject> Milist;

    private void Awake()
    {
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
        if (Input.GetKeyDown(KeyCode.B))
        {
            MissileLaunchStart();
        }
    }

    //ミサイル発射
    public void MissileLaunchStart()
    {
        if (GameManager.instance.TargetEnemyList.Count == 0)
            return;

        Milist = GameManager.instance.TargetEnemyList;
        for (int i=0; i < missiles_pack.Length; i++)
        {
            StartCoroutine(MissileLaunch(missiles_pack[i].transform));
        }
    }


    //ミサイル発射中
    private IEnumerator MissileLaunch(Transform BasePoss)
    {
        for(int x=0; x < Width; x++) //横
        {
            for(int y = 0; y < VerWidth; y++) //縦
            {
                Transform tr = BasePoss;
                tr.position = tr.transform.position + new Vector3(x * Interval, y * Interval, 0);

                GameObject missle = Instantiate(missile, new Vector3(x * Interval, y * Interval, 0) + BasePoss.position, BasePoss.rotation) as GameObject;
                //Missile missile_c = missile.GetComponent<Missile>();
                //missile_c.Shoot(SelectMis(x + y));
                Missile_Bullet missile_c = missile.GetComponent<Missile_Bullet>();
                missile_c.SetTarget(SelectMis(x + y));

                yield return new WaitForSeconds(0.1f);
                //yield return null;
            }
        }

    }

    //オブジェクトを選ぶ
    private GameObject SelectMis(int i)
    {
        //List<GameObject> Mlist = GameManager.instance.TargetEnemyList;
        if (Milist.Count < 0) return new GameObject();


        int index = 0;
        try
        {
            index = i % Milist.Count;
        } catch { }
        return Milist[index];

    }

}
