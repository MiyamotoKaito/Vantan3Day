using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 入国審査
/// </summary>
public class ImmigrationInspection : MonoBehaviour
{
    [Header("放置時間")]
    [SerializeField] private float _neglectTime;
    private float _neglectTimer; //放置時間のタイマー
    [Header("放置時間のデバッグUI")] 
    [SerializeField] private TextMeshProUGUI _neglectText;
    [Header("PlayerController")]
    [SerializeField] private PlayerController _playerController;
    
    /// <summary>
    /// 入力した審査の結果
    /// </summary>
    private ExaminationType _examinationType;

    private VisitorManager _visitorManager;
   
    private void Awake()
    {
        _visitorManager = FindObjectOfType<VisitorManager>();
        _visitorManager.OnNeglectSet += SetNeglectTimer;
        _visitorManager.OnNeglectSet?.Invoke();
        if(_playerController == null) return;
        _playerController.ButtonPressed += (type) =>
        {
            if (_visitorManager.IsExaminationInput)
            {
                ConductAnExamination(type);
            }
        };
    }

    private void Update()
    {
        //TODO：デバッグ用のビルド前には消しておく
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log("a");
            ConductAnExamination(ExaminationType.Ok);
        }
        NeglectTimeUpdate();
    }

    /// <summary>
    /// 放置タイマーの更新
    /// </summary>
    private void NeglectTimeUpdate()
    {
        var visitor = _visitorManager.CurrentVisitor;
        if(visitor == null || !_visitorManager.IsNeglectTimeStart) return;
        //タイマーを減算
        _neglectTimer -= Time.deltaTime;
        _neglectText.text = _neglectTimer.ToString("0.0");
        if (_neglectTimer <= 0) //放置処理の実行
        {
            visitor.ExaminationNeglect();
            
            _neglectTimer = 0;
            _visitorManager.SetNeglectTimeFlag(false);
        }
    }

    /// <summary>
    /// 放置タイマーの設定
    /// </summary>
    private void SetNeglectTimer()
    {
        _neglectTimer = _neglectTime;
        _neglectText.text = _neglectTimer.ToString("0.0");
    }
    
    /// <summary>
    /// 審査内容の判定
    /// </summary>
    /// <param name="type">審査方法</param>
    private void ConductAnExamination(ExaminationType type)
    {
        var data = _visitorManager.CurrentVisitor;
        if(data == null) return;
        switch (type)
        {
            case ExaminationType.Ok:
                data.ExaminationOk();
                break;
            case ExaminationType.Ng:
                data.ExaminationNg();
                break;
            case ExaminationType.Blockade:
                data.ExaminationBlockade();
                break;
        }
    }
}
