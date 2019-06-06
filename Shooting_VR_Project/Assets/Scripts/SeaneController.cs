using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeaneController : MonoBehaviour
{

    public static string[] _sceneSequence;
    
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("ActiveScene : " + scene.name);
    }
}
