using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score_Board : MonoBehaviour
{
    [SerializeField]
    GameObject[] text;

    [SerializeField]
    TextMesh textMesh;
    

    // Start is called before the first frame update
    void Start()
    {
        


        textMesh.text = "撃破数 "+ GameManager.instance.downCount + "体";
    }

    float time = 0;
    int i = 0;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GameState != 2) return;

        time += Time.deltaTime;
        if (time > 0.5 && i > -1)
        {
            if (0 <= i && i < text.Length)
            {
                text[i].SetActive(true);
                i++;
            }
            else
            {
                i = -1;
            }
            time = 0;
        }

        if (i == -1)
        {
            if (time < 1)
            {
                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, time);
            }
            else
            {
                i = -2;
                time = 0;
            }
        }

        if (i == -2)
        {
            if (time < 3)
            {
                //SceneManager.UnloadSceneAsync("wave6");
                var o = GameObject.Find("MissileManager");
                SceneManager.MoveGameObjectToScene(o, SceneManager.GetActiveScene());
                var o2 = GameObject.Find("SceneManager");
                SceneManager.MoveGameObjectToScene(o2, SceneManager.GetActiveScene());


                Destroy(o);
                Destroy(o2);
                SceneManager.LoadScene("Title");
                //SceneManager.UnloadSceneAsync("wave6");
            }
        }


    }
}
