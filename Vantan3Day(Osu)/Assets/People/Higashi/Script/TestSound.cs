using System;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public void OpenSE()
    {
        SoundManager.Instance.PlaySE(SoundManager.SEType.ShatterOpen);
    }
    public void CloseSE()
    {
        SoundManager.Instance.PlaySE(SoundManager.SEType.ShatterClose);
    }
}
