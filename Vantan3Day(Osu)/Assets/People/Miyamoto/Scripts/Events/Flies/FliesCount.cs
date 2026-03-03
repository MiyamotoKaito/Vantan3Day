using System;
using System.Collections.Generic;
using UnityEngine;

public class FliesCount : IDisposable
{
    private int _count;
    private List<Fly> _flies = new List<Fly>();
    //private EmargencyButton _emargencyButton;
    public FliesCount()
    {
        _count = 0;
        //_emargencyButton = emargencyButton;
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
        Debug.Log($"ハエのがボタンを押した");
        if (_count <= 3)
        {

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
