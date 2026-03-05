using System;
using UnityEngine;

/// <summary>
/// インゲーム内の管理
/// </summary>
public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;
    [Header("VisitorManager")]
    [SerializeField] private VisitorManager _visitorManager;
    [Header("GameOverUI")]
    [SerializeField] private GameObject _gameOverUI;
    
    /// <summary>
    /// GameOver処理
    /// </summary>
    public Action OnGameOver;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            OnGameOver += GameOver;
        }
        else
        {
            Destroy(this);
            OnGameOver -= GameOver;
        }
        _visitorManager.IsNeglectTimeStart = false;
        _gameOverUI.SetActive(false);
    }
    
    /// <summary>
    /// GameOver処理
    /// </summary>
    private void GameOver()
    {
        _gameOverUI.SetActive(true);
        Debug.Log("ゲームオーバー処理");
    }
}
