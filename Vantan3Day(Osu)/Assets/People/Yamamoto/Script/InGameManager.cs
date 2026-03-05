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
    [Header("GameClearUI")]
    [SerializeField] private GameObject _gameClearUI;
    [Header("GameOverUI")]
    [SerializeField] private GameObject _gameOverUI;

    /// <summary>
    /// GameClear処理
    /// </summary>
    public Action OnGameClear;
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
            OnGameClear += GameClear;
        }
        else
        {
            Destroy(this);
            OnGameOver -= GameOver;
            OnGameClear -= GameClear;
        }
        _visitorManager.IsNeglectTimeStart = false;
        _gameOverUI.SetActive(false);
        _gameClearUI.SetActive(false);
    }

    /// <summary>
    /// GameClear処理
    /// </summary>
    private void GameClear()
    {
        _gameClearUI.SetActive(true);
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
