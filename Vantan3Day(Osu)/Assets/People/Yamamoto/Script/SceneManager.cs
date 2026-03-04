using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン管理
/// ・タイトルから配置する
/// </summary>
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    
    [Header("シーン名")]
    [SerializeField] private List<string> _sceneNames;
    /// <summary>
    /// シーン遷移
    /// </summary>
    public Action<string> OnSceneLoaded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        OnSceneLoaded += LoadScene;
    }

    /// <summary>
    /// 一致したシーン名を開く
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    private void LoadScene(string sceneName)
    {
        foreach (var names in _sceneNames)
        {
            if(names == sceneName) UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
    
