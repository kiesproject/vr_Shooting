using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : AirFighter
{
    //[SerializeField] int HP = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(int damage)
    {
        Debug.Log("Hit!!");

        Damage((float)damage);

        Debug.Log(hp);

        //if (HP <= 0)
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
