using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
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
        _timer = _clearTime;
        _isCleared = false;
        _closeTimer = false;
    }

}
