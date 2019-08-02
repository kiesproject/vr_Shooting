using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chenge_Weapon_UI : MonoBehaviour
{
    [SerializeField]
    GameObject missileUI;
    [SerializeField]
    GameObject laserUI;

    GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.Weapon == 0)
        {
            laserUI.SetActive(true);
            missileUI.SetActive(false);
        }
        else if (GM.Weapon == 1)
        {
            laserUI.SetActive(false);
            missileUI.SetActive(true);
        }
    }
}
