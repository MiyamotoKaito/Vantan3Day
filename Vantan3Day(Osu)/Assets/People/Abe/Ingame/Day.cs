using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour
{
    public static int _dayCount = 1;
    [System.Serializable]
    public class BGMData
    {
        public int DayCount;
        public AudioClip BGM;
    }

    [SerializeField]
    private int _maxDayCount = 4;
    [SerializeField]
    Timer _timer;
    [SerializeField]
    private List<BGMData> _data;
    private Dictionary<int, AudioClip> _bgmDic = new Dictionary<int, AudioClip>();
    private void Start()
    {
        _timer = FindAnyObjectByType<Timer>();
        _timer.OnTimerFinished += FinishDay;
        foreach (var data in _data)
        {
            _bgmDic[data.DayCount] = data.BGM;
        }
        SoundManager.Instance.PlayBGM(_bgmDic[_dayCount]);
    }



    private void FinishDay()
    {
        SoundManager.Instance.StopBGM();
        _dayCount++;
        if (_dayCount >= _maxDayCount)
        {
            SceneManager.Instance.OnSceneLoaded?.Invoke("ClearScene");
            return;
        }
        SceneManager.Instance.OnSceneLoaded?.Invoke("MasterScene");
        _timer.ResetTimer();
        Debug.Log("Day " + _dayCount);
    }


}
