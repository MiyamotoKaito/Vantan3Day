using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// 来訪者の管理
/// 人間、宇宙人、改造人間をランダムで来訪させる
/// </summary>
public class VisitorManager : MonoBehaviour
{
    [Header("全ての来訪者のデータ")]
    [SerializeField] private List<VisitorBaseData> _visitors;
    /// <summary>
    /// 来訪者の設定
    /// 審査が終了後、呼び出す
    /// </summary>
    public Action onVisitor;
    /// <summary>
    /// 現在の来訪者を保持
    /// </summary>
    public VisitorBaseData CurrentVisitor { get; private set; }

    [Header("デバッグ用のImage")] public Image image;

    private void Awake()
    {
        onVisitor += () =>
        {
            VisitorGenerate();
            VisitorsSettings();
        };
        onVisitor?.Invoke();
    }

    /// <summary>
    /// 来訪者を生成
    /// </summary>
    private void VisitorGenerate()
    {
        var random = Random.Range(0, _visitors.Count);
        CurrentVisitor = _visitors[random];
    }

    /// <summary>
    /// 来訪者の設定
    /// </summary>
    private void VisitorsSettings()
    {
        image.sprite = CurrentVisitor.Sprite;
    }
}
