using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [System.Serializable]
    public class DayTimer
    {
        [Min(1)]
        public int DayCount;
        public int Time;
    }
    [SerializeField]
    private List<DayTimer> _timerList;
    private float _clearTime;
    private float _timer;
    private bool _isCleared;
    private bool _closeTimer;
    public event Action OnTimerFinished;

    private void Start()
    {
        ResetTimer();
    }
    private void Update()
    {
        if (_closeTimer)
        {
            return;
        }
        if (_isCleared)
        {
            _closeTimer = true;
            OnTimerFinished?.Invoke();
            return;
        }
        if (_timer <= 0f)
        {
            _isCleared = true;
            return;
        }
        _timer -= Time.deltaTime;
    }
    public float GetClearTime()
    {
        return _clearTime;
    }
    public float GetTime()
    {
        return _timer;
    }

    public void ResetTimer()
    {
        foreach (var timer in _timerList)
        {
            if (Day._dayCount == timer.DayCount)
            {
                _clearTime = timer.Time;
                _timer = timer.Time;
                break;
            }
        }
        _isCleared = false;
        _closeTimer = false;
    }

}
