using System.Collections.Generic;
using UnityEngine;

// SEの種類を決めれるサウンドマネージャー
public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    // BGM用AudioSource
    private AudioSource _bgmSource;

    //インスペクターに表示する
    [System.Serializable]
    public class SEData
    {
        public SEType type;      // SEの種類
        public AudioClip clip;   // 音データ
    }

    public List<SEData> SeList = new List<SEData>();

    private Dictionary<SEType, AudioClip> _seDictionary;
    void Awake()
    {
        //一つだけ
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // 自分をInstanceにする
        Instance = this;

        // シーンをまたいでも消えないようにする
        DontDestroyOnLoad(gameObject);

        _seDictionary = new Dictionary<SEType, AudioClip>();

        foreach (SEData data in SeList)
        {
            _seDictionary[data.type] = data.clip;
        }


        // 自分についているAudioSourceを取得（BGM用）
        _bgmSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// BGMを再生するメソッド
    /// </summary>
    /// <param name="clip"></param>
    public void PlayBGM(AudioClip clip)
    {
        // もし同じ曲が流れていたら戻す
        if (_bgmSource.clip == clip) return;

        // 曲をセット
        _bgmSource.clip = clip;

        // ループON
        _bgmSource.loop = true;

        // 再生
        _bgmSource.Play();
    }
    /// <summary>
    /// BGMを止める
    /// </summary>
    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    /// <summary>
    /// SEを再生してそのSEをけす。
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySE(SEType type)
    {
        if (!_seDictionary.ContainsKey(type))
        {
            Debug.LogWarning($"{type}が登録されていません！");
            return;
        }
        // 新しくAudioSourceを作る
        AudioSource seSource = gameObject.AddComponent<AudioSource>();

        // 辞書から再生する音をセットする
        seSource.clip = _seDictionary[type];

        // ループしない
        seSource.loop = false;

        // 再生
        seSource.Play();

        // 再生が終わったら削除
        Destroy(seSource, seSource.clip.length);
    }
    public enum SEType
    {
        Stamp,
        ButtonPush,
        ShatterOpen,
        ShatterClose,
        WindowOpen,
        WindowClose,
    }
}


