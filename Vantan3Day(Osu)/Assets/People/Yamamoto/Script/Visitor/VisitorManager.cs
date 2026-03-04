using System;
using System.Collections.Generic;
using DG.Tweening;
using Ingame;
using Unity.Collections.LowLevel.Unsafe;
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
    [Header("UIの設定")] 
    [Header("待機場所")] 
    [SerializeField] private Transform _awaitPos;
    [Header("入国場所")]
    [SerializeField] private Transform _entryPos;
    [Header("退場場所")]
    [SerializeField] private Transform _exitPos;
    [Header("戻る場所")]
    [SerializeField] private Transform _backPos;
    [Header("アニメーション時間")]
    [SerializeField] private float _animTime;
    [Header("バッテリー")]
    [SerializeField] private Battely _battely;
    [Header("UI")]
    [Header("来訪者Image")] 
    [SerializeField] private Image _visitorImage;
    [Header("来訪者情報カード")]
    [SerializeField] private Image _visitorInfoCard;
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
    /// 来訪者が立ち去る
    /// </summary>
    public Action OnLeave;
    /// <summary>
    /// 来訪者が戻る
    /// </summary>
    public Action OnGoBack;
    /// <summary>
    /// 物を投げつけるまでの一連の流れ
    /// </summary>
    public Action OnThingThrow;
    /// <summary>
    /// 放置時間の設定
    /// </summary>
    public Action OnNeglectSet;
    /// <summary>
    /// バッテリーの変動
    /// </summary>
    public Action  OnBattely;
    /// <summary>
    /// 来訪者の情報カードの表示切替
    /// true：表示　false：非表示
    /// </summary>
    public Action<bool> OnVisitorInfoCard;
    /// <summary>
    /// 現在の来訪者を保持
    /// </summary>
    public VisitorBaseData CurrentVisitor { get; private set; }
    /// <summary>
    /// 審査の入力が可否
    /// true：可能　false：不可能
    /// </summary>
    public bool IsExaminationInput { get; private set; }
    /// <summary>
    /// 放置タイマーの開始
    /// true：開始　false：終止
    /// </summary>
    public bool IsNeglectTimeStart {get; private set; }

    private void Awake()
    {
        OnVisitor += () =>
        {
            VisitorGenerate();
            VisitorsSettings();
        };
        OnEntry += VisitorsEntry;
        OnExit += VisitorsExit;
        OnLeave += VisitorsLeave;
        OnGoBack += VisitorsGoBack;
        if (_battely != null)
        {
            OnBattely += _battely.TriggerEvent;
        }
        OnVisitorInfoCard += VisitorInfoCardSwitch;
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
    /// 来訪者のカード表示切替
    /// </summary>
    /// <param name="flag">true：表示　false：非表示</param>>
    private void VisitorInfoCardSwitch(bool flag)
    {
        _visitorInfoCard.enabled = flag;
        Debug.LogWarning(flag);
    }

    /// <summary>
    /// 差分を切替
    /// </summary>
    /// <param name="sp">差分</param>>
    public void VisitorFaceChange(Sprite sp)
    {
        _visitorImage.sprite = sp;
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
            .Append(_visitorImage.DOColor(Color.white, 0))
            .OnComplete(() =>
            {
                SetNeglectTimeFlag(true);
                OnNeglectSet?.Invoke();
            });
    }

    /// <summary>
    /// 退場のアニメーション
    /// </summary>
    private void VisitorsExit()
    {
        _visitorImage.transform.DOMove(_exitPos.position, _animTime).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                SetNeglectTimeFlag(false);
                OnNeglectSet?.Invoke();
            });
    }

    /// <summary>
    /// 立ち去るアニメーション
    /// </summary>
    private void VisitorsLeave()
    {
        //段々、透明にしていく
        DOTween.ToAlpha(() =>
            _visitorImage.color, color => _visitorImage.color = color, 0, _animTime)
            .OnComplete(() =>
            {
                SetNeglectTimeFlag(false);
                OnNeglectSet?.Invoke();
            });
    }

    /// <summary>
    /// 戻るアニメーション
    /// </summary>
    private void VisitorsGoBack()
    {
        _visitorImage.transform.DOMove(_backPos.position, _animTime).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                SetNeglectTimeFlag(false);
                OnNeglectSet?.Invoke();
            });
    }

    /// <summary>
    /// 放置タイマー開始のフラグを設定
    /// </summary>
    /// <param name="flag">true：開始　false：終止</param>
    public void SetNeglectTimeFlag(bool flag)
    {
        IsNeglectTimeStart = flag;
    }
}
