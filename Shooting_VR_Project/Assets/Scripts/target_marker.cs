using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_marker : MonoBehaviour
{
    GameManager GM;
    GameObject target;

    bool visible = false; //写っているかどうか
    float dis = 12;

    private void Awake()
    {
        //GM = GameManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
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

            if (visible)
            {
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
    }
    
    //画面に写ってる時に
    private void OnWillRenderObject()
    {

        //Debug.Log("見えてる :" + this.gameObject.name);
        visible = false;
        if (Camera.current.name == "Main Camera")
        {
            visible = true;
        }
    }

    private void OnDestroy()
    {
        GM.TargetEnemyDead(target);
    }
}
