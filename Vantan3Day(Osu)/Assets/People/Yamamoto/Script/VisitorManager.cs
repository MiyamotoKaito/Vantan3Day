using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
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
    [Header("UI関連")] 
    [Header("待機場所")] 
    [SerializeField] private Transform _awaitPos;
    [Header("入国場所")]
    [SerializeField] private Transform _entryPos;
    [Header("退場場所")]
    [SerializeField] private Transform _exitPos;
    [Header("アニメーション時間")]
    [SerializeField] private float _animTime;
    public float AnimTime => _animTime;
    /// <summary>
    /// 来訪者の設定
    /// 審査が終了後、呼び出す
    /// </summary>
    public Action OnVisitor;
    /// <summary>
    /// 来訪者の入国
    /// </summary>
    public Action OnEntry;
    /// <summary>
    /// 来訪者の退場
    /// </summary>
    public Action OnExit;
    /// <summary>
    /// 物を投げつけるまでの一連の流れ
    /// </summary>
    public Action OnThingThrow;
    /// <summary>
    /// 現在の来訪者を保持
    /// </summary>
    public VisitorBaseData CurrentVisitor { get; private set; }
    /// <summary>
    /// 審査の入力が可否
    /// true：可能　false：不可能
    /// </summary>
    public bool IsExaminationInput { get; private set; }
    
    [Header("来訪者")] 
    [SerializeField] private Image _visitorImage;

    private void Awake()
    {
        OnVisitor += () =>
        {
            VisitorGenerate();
            VisitorsSettings();
        };
        OnEntry += VisitorsEntry;
        OnExit += VisitorsExit;
        OnVisitor?.Invoke();
    }

    /// <summary>
    /// 審査入力の可否を決定する
    /// </summary>
    /// <param name="flag">true：可能　false：不可能</param>
    public void SetInput(bool flag)
    {
        IsExaminationInput = flag;
    }

    /// <summary>
    /// 来訪者を生成
    /// </summary>
    private void VisitorGenerate()
    {
        if(_visitors.Count == 0) return;
        var random = Random.Range(0, _visitors.Count);
        CurrentVisitor = _visitors[random];
        CurrentVisitor.Visit(this, _animTime);
    }

    /// <summary>
    /// 来訪者の設定
    /// </summary>
    private void VisitorsSettings()
    {
        _visitorImage.sprite = CurrentVisitor.Sprite;
    }

    /// <summary>
    /// 待機場所に設置
    /// </summary>
    public void VisitorAwaitSet()
    {
        _visitorImage.transform.position = _awaitPos.position;
    }

    /// <summary>
    /// 入国のアニメーション
    /// </summary>
    private void VisitorsEntry()
    {
        _visitorImage.color = Color.black;
        var sq = DOTween.Sequence();
        sq.Append(_visitorImage.transform.DOMove(_entryPos.position, _animTime).SetEase(Ease.Linear))
            .Append(_visitorImage.DOColor(Color.white, 0));
    }

    /// <summary>
    /// 退場のアニメーション
    /// </summary>
    private void VisitorsExit()
    {
        _visitorImage.transform.DOMove(_exitPos.position, _animTime).SetEase(Ease.Linear);
    }
}
