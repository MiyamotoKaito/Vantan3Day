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
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _isCleared = true;
        }
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
