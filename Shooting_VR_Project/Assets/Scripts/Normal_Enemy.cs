using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_Enemy : AirFighter
{
    // Start is called before the first frame update
    protected override void Start()
    {
        max_hp = 6;
        hp = max_hp;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
