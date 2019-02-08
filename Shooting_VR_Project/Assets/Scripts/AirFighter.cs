using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public abstract class AirFighter : MonoBehaviour
{
    //戦闘機のHP
    public float hp;

    //死亡したかどうか
    private void Down_Chack()
    {
       if (this.hp < 0)
        {
            hp = 0;
            Shooting_down();
        }
    }

    //死亡
    private virtual protected void Shooting_down()
    {
        //HPがゼロになった時の処理
    }

    //ダメージを与える(敵からの攻撃)
    public void Damage(float damage)
    {
        //HPからダメージ分減らす
        hp -= damage;

        //撃墜判定
        Down_Chack();
    }




}


