using System;
using UnityEngine;

/// <summary>
/// インゲーム内の管理
/// </summary>
public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;
    
    /// <summary>
    /// GameOver処理
    /// </summary>
    public Action OnGameOver;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        OnGameOver += GameOver;
    }
    
    /// <summary>
    /// 仮のゲームオーバー処理
    /// ゲームオーバー時に起こる処理の中身を記述
    /// </summary>
    private void GameOver()
    {
        Debug.LogWarning("ゲームオーバー");
    }
}
