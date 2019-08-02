using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SeaneController : MonoBehaviour
{
    public static SeaneController sceanController;

    //現在のシーンの配列のindex用
    static int nowSceanIndex = 0;

    //テスト用にウェーブごとで一時停止する
    [SerializeField]
    bool testFlag = false;
    [SerializeField]
    string testText;
    GameObject PB; //

    //切り替えるためのシーンの名前（文字列）
    [SerializeField] string[] _sceneSequence;

    //デストロイできないオブジェクトに指定
    private void Awake()
    {
        if (sceanController == null) sceanController = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        nowSceanIndex = 0;

        SceneLoad();

        //先頭のシーン以外はアンロード(1からスタート)
        for (int i = 1; i < _sceneSequence.Length; i++)
        {
            SceneManager.UnloadSceneAsync(_sceneSequence[i]);
        }

        //デバッグ用
        PB = GameObject.FindGameObjectWithTag("PlayerBase");

    }

    private void Update()
    {
        //Debug.Log("nowSceanIndex " + nowSceanIndex);
        //テスト用にNキーでシーン切り替え
        //if (Input.GetKeyDown(KeyCode.N)) SwitchScean();
    }

    public void SceneLoad()
    {
        try
        {
            SceneManager.LoadSceneAsync(_sceneSequence[nowSceanIndex], LoadSceneMode.Additive);
        }
        catch { Debug.Log("Scene Load Failed"); }
    }


    //シーン切り替えメソッド(外部参照可)
    public void SwitchScean()
    {



        if (nowSceanIndex != _sceneSequence.Length - 1)
        {
#if UNITY_EDITOR
            if (testFlag)
            {
                if (_sceneSequence[nowSceanIndex + 1] == testText && testText!=null)
                {
                    Scene_Load();
                    EditorApplication.isPaused = true;
                    if (PB == null)
                        Debug.Log("sore");
                    PB.GetComponent<PlayerBase>().SetSpeed_forDebug(0.25f);
                }
                else
                {
                    Scene_Unload();
                }
                nowSceanIndex++;
                return;
            }
#endif
            Scene_Load();
            Scene_Unload();
            nowSceanIndex++;

            
        }
    }

    void Scene_Load()
    {
        try
        {
            SceneManager.LoadSceneAsync(_sceneSequence[nowSceanIndex + 1], LoadSceneMode.Additive);
        }
        catch { Debug.Log("Scene Load Failed"); }
    }

    void Scene_Unload()
    {
        try
        {
            SceneManager.UnloadSceneAsync(_sceneSequence[nowSceanIndex]);
        }
        catch { Debug.Log("Scene UnLoad Failed"); }
    }

}
