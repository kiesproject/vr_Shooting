using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_marker : MonoBehaviour
{
    GameManager GM = GameManager.instance;

    bool visible = false; //写っているかどうか

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //GameManager.instance.TargetEnemyList.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (visible)
        {
            //リストに入れる
            if (!GameManager.instance.TargetEnemyList.Contains(this.gameObject))
                GameManager.instance.TargetEnemyList.Add(this.gameObject);
        }
        else
        {
            //リストから除く
            if (GameManager.instance.TargetEnemyList.Contains(this.gameObject))
                GameManager.instance.TargetEnemyList.Remove(this.gameObject);
        }
    }

    //画面に写ってる時に
    private void OnWillRenderObject()
    {
        visible = false;
        if (Camera.current.name == "Main Camera")
        {
            visible = true;
        }
    }
}
