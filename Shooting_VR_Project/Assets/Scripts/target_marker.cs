using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_marker : MonoBehaviour
{
    GameManager GM;
    GameObject target;
    GameObject MissileTarget;
    GameObject missile_crrent_target;

    bool visible = false; //写っているかどうか
    float dis = 30;

    private void Awake()
    {
        //GM = GameManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        //MissileTarget = Resources.Load("MissileTarget") as GameObject;
        GM = GameManager.instance;
        var t = this.transform.parent;
        while (true)
        {
            if (t == null) break;

            if (t.GetComponent<AirFighter>() == null)
            {
                t = t.transform.parent;
            }
            else
            {
                target = t.gameObject;
                break;
            }
        }
        //DontDestroyOnLoad(gameObject);
        //GameManager.instance.TargetEnemyList.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.Player == null) return;
        if (Vector3.Distance(transform.position, GM.Player.transform.position) <= dis)
        {
            Debug.Log("visible:" + visible);
            if (visible)
            {
                Debug.Log("3333");
                //リストに入れる
                if (!GM.TargetEnemyList.Contains(target))
                    GM.TargetEnemyList.Add(target);
            }
            else
            {
                //リストから除く
                if (GM.TargetEnemyList.Contains(target))
                    GM.TargetEnemyList.Remove(target);
            }
        }
        //visible = false;

        /*
        if (visible == true )
        {
            
            if (GM.Weapon == 1 )
            {
                if (missile_crrent_target == null)
                {
                    missile_crrent_target = Instantiate(MissileTarget, transform.position, Quaternion.identity);
                }
                Debug.Log("でた");
            }
            else
            {
                Debug.Log("えるす");
                if (missile_crrent_target != null)
                {
                    Destroy(missile_crrent_target);
                    missile_crrent_target = null;
                    Debug.Log("けした");
                }
            }
        }*/
        
    }
    
    //画面に写ってる時に
    private void OnWillRenderObject()
    {
        
        
        //visible = false;
        if (Camera.current.name == "Camera")
        {
            Debug.Log("見えてる :" + this.gameObject.name);
            visible = true;
        }
    }

    private void OnDestroy()
    {
        GM.TargetEnemyDead(target);
    }
}
