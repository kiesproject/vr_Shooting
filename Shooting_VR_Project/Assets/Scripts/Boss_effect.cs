using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_effect : MonoBehaviour
{
    [SerializeField]
    private GameObject nabebuta;
    private int state=0;
    private float timeCounter = 0;
    private bossAttack boss;

    [SerializeField]
    GameObject targetPoss;
    [SerializeField]
    GameObject start_poss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<bossAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime/5;
        /*
        if (state == 0) //ゆっくり降下
        {
            Debug.Log("targetPoss: " + targetPoss+"start_poss: "+start_poss);
            boss.transform.position = Vector3.Lerp(start_poss.transform.position, targetPoss.transform.position , timeCounter);

            if (timeCounter >= 1)
            {
                state = 1;
                timeCounter = 0;
            }
        }*/
        
        if (state == 0) //なべぶたが開く
        {
            nabebuta.transform.localPosition = Vector3.Lerp(nabebuta.transform.localPosition, nabebuta.transform.localPosition + new Vector3(0, 0.0005f, 0), timeCounter );
            if (timeCounter >= 1)
            {
                state = 2;
                timeCounter = 0;
            }
        }

        if (state == 2)
        {
            transform.Rotate(Vector3.up * 3); 
            timeCounter = 0;
            boss.first_poss = transform.position;
            boss.enabled = true;
            boss.boss_stopFlag = true;
        }

    }
}
