using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeaneController : MonoBehaviour
{
    public static SeaneController sceanController;

    //現在のシーンの配列のindex用
    static int nowSceanIndex = 0;

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
        //先頭のシーン以外はアンロード(1からスタート)
        for (int i = 1; i < _sceneSequence.Length; i++)
        {
            SceneManager.UnloadScene(_sceneSequence[i]);
        }
    }

    private void Update()
    {
        //Debug.Log("nowSceanIndex " + nowSceanIndex);
        //テスト用にNキーでシーン切り替え
        if (Input.GetKeyDown(KeyCode.N)) SwitchScean();
    }

    //シーン切り替えメソッド(外部参照可)
    public void SwitchScean()
    {
        if(nowSceanIndex != _sceneSequence.Length - 1)
        {
            SceneManager.LoadSceneAsync(_sceneSequence[nowSceanIndex + 1],LoadSceneMode.Additive);
            SceneManager.UnloadScene(_sceneSequence[nowSceanIndex]);
            nowSceanIndex++;
        }
    }
}
