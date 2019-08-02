using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class missileManager : MonoBehaviour
{
    public Image MGage;
    public GameObject Rimage;
    public GameObject Nimage;

    public float maxMissile = 100;
    public float currentMissile = 100;
    bool flag = false;

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
        MGage = GameObject.Find("Mgage").GetComponent<Image>();
        Rimage = GameObject.FindGameObjectWithTag("redry_img");
        Nimage = GameObject.FindGameObjectWithTag("NChr_img");
    }

    // Update is called once per frame
    void Update()
    {
        if (!flag)
        {
            MGage.fillAmount += 0.1f * Time.deltaTime;
            MGage.color = new Color(1.0f, 0.0f, 0.0f);
        }
        else
        {
            MGage.fillAmount -= 0.5f * Time.deltaTime;
        }
        if (MGage.fillAmount <= 0 && flag)
        {
            flag = false;
        }

        if (MGage.fillAmount > 0.999f)
        {
            MGage.color = new Color(0.4f, 1.0f, 0.4f);
            Rimage.SetActive(true);
            Nimage.SetActive(false);
        }
        else
        {
            Rimage.SetActive(false);
            Nimage.SetActive(true);
        }

        if (GameManager.instance.TargetEnemyList.Count == 0) return;

        //if (Input.GetKeyDown(KeyCode.B) && !flag && MGage.fillAmount > 1 / maxMissile * 99)
        if (GameManager.instance.Missile_Trigger && !flag && MGage.fillAmount > 1 / maxMissile * 99)
            {
            flag = true;
            MGage.color = new Color(1.0f, 0.0f, 0.0f);
            MissileLaunchStart();
            //GameManager.instance.Missile_Trigger = false;
        }
    }

    //ミサイル発射
    public void MissileLaunchStart()
    {
        if (GameManager.instance.TargetEnemyList.Count == 0)
            return;

        Milist = new List<GameObject>(GameManager.instance.TargetEnemyList);
        for (int i=0; i < missiles_pack.Length; i++)
        {
            StartCoroutine(MissileLaunch(missiles_pack[i].transform));
        }
    }


    //ミサイル発射中
    private IEnumerator MissileLaunch(Transform BasePoss)
    {
        //Debug.Log("発射中");
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
                if (missile_c == null) Debug.Log("入ってない");
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
