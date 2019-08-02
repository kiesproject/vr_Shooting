using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    bool play = false;
    AudioSource AS;
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (play == true)
        {
            if (AS.isPlaying == false)
            {
                Destroy(this.gameObject);
            }
        }

        if (AS.clip == null) Destroy(this.gameObject);
    }

    //プレイする
    public void GoPlay()
    {
        AS = GetComponent<AudioSource>();
        play = true;
        AS.Play();
    }
}
