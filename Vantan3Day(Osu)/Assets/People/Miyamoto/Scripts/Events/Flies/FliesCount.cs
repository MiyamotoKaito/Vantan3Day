using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FliesCount : IDisposable
{
    private int _count;
    private int _maxCount;
    private List<Fly> _flies = new List<Fly>();
    private EmargencyButton _emargencyButton;
    public FliesCount(EmargencyButton button, int maxCount)
    {
        _count = 0;
        _emargencyButton = button;
        _maxCount = maxCount;
    }
    /// <summary>
    /// 
    /// </summary>
    public void Register(Fly fly)
    {
        _flies.Add(fly);
        fly.FliesCountChanged += FliesCountChanged;
    }
    /// <summary>
    /// ハエのボタンによって、ハエの数が変化したときに呼び出されるメソッド
    /// </summary>
    /// <param name="value"></param>
    private void FliesCountChanged(int value)
    {
        _count += value;

        if (_count >= _maxCount)
        {
            Debug.Log($"ハエがボタンを押した");
            _emargencyButton.ButtonPush().Forget();
        }
    }

    public void Dispose()
    {
        foreach (var fly in _flies)
        {
            fly.FliesCountChanged -= FliesCountChanged;
        }
    }
}
