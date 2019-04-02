using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BarrierGenerator : MonoBehaviour
{
    public GameObject[] Beacons;

    public PlayableDirector playableDirector;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Beacons = GameObject.FindGameObjectsWithTag("Beacon");

        if (Beacons.Length <= 0)
        {
            Debug.Log("break!!");
            playableDirector.Play();
        }
    }
}
