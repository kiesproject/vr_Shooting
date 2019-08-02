using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GRgage : MonoBehaviour
{
    public Image lifeGage;
    public Image lifeGageRed;
    private Player player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player.GetComponent<Player>();

        //緑HPバーの取得
       lifeGage = GameObject.Find("LifeGage").GetComponent<Image>();
        //赤HPバーを取得
        lifeGageRed = GameObject.Find("LifeGageRed").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //現在のHPの値を緑HPバーで表示
        lifeGage.fillAmount = player.Get_Hp() / player.Get_Max_Hp();
        //現在のHPの値を赤HPバーで表示
        if (lifeGageRed.fillAmount > 1 / player.Get_Max_Hp() * player.Get_Hp())
        {
            lifeGageRed.fillAmount -= 0.01f;
        }
    }
}
