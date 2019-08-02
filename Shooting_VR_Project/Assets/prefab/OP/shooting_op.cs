using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shooting_op : MonoBehaviour
{
    public GameObject text;
    public float timer;
    public GameObject text2;
    // Start is called before the first frame update
    void Start()
    {
        text.active = true;
        text2.active = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.8f)
        {
            text.active = false;
        }
        if (timer >= 2.0f)
        {
            text2.active = true;
        }

        if (timer >= 5.0f)
        {
            text2.active = false;
        }

        if (timer >= 8.0f)
        {
            SceneManager.LoadScene("Main");
        }

    }
}
