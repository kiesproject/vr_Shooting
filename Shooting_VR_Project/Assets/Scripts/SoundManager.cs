using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    AudioClip[] audios;

    [SerializeField]
    string[] filename;

    [SerializeField]
    GameObject soundObject;

    Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();
    AudioSource source;


    //一番最初に実行
    private void Awake()
    {
        //ゲームマネージャーにアクセス出来るようにする。
        if (instance == null)
        { instance = this; }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        for (int i=0; i < filename.Length; i++)
        {
            try
            {
                audioDict[filename[i]] = audios[i];
            }
            catch
            {
                Debug.Log("宣言されていないのでは？？");
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //音を生成
    public void Instance_Sound(Vector3 wroldPoos, string name, float pitch)
    {
        GameObject s = Instantiate(soundObject, wroldPoos, Quaternion.identity);
        AudioSource audioSource = s.GetComponent<AudioSource>();
        audioSource.pitch = pitch;

        try
        {
            audioSource.clip = audioDict[name];
        }
        catch
        {
            Debug.Log("音楽ファイルが見つかりません");
        }

        if (audioSource.clip != null)
        {
            s.GetComponent<SoundObject>().GoPlay();
        }

    }

    //BGMを再生
    public void PlayBGM()
    {
        try
        {
            source.clip = audioDict[name];
        }
        catch
        {
            Debug.Log("音楽ファイルが見つかりません");
        }

        if (source.clip != null)
        {
            source.Play();
        }

    }

}
