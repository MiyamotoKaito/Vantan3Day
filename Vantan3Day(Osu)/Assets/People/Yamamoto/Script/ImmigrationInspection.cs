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

    /// <summary>
    /// 入国審査
    /// プレイヤーが呼び出す
    /// </summary>
    public Action<ExaminationType> OnExamination;
    
    /// <summary>
    /// 入力した審査の結果
    /// </summary>
    private ExaminationType _examinationType;

    private VisitorManager _visitorManager;
   
    private void Awake()
    {
        _visitorManager = FindObjectOfType<VisitorManager>();
        OnExamination += (type) =>
        {
            if (_visitorManager.IsExaminationInput)
            {
                ConductAnExamination(type);
            }
        };
        _visitorManager.OnNeglectSet += SetNeglectTimer;
        _visitorManager.OnNeglectSet?.Invoke();
    }

    private void Update()
    {
        NeglectTimeUpdate();
        ReviewInput();
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
    /// 審査の入力
    /// </summary>
    private void ReviewInput()
    {
        //TODO：ここはプレイヤーに処理が出来るまで、仮の入力を実装しておく
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            _examinationType = ExaminationType.Ok;
            OnExamination?.Invoke(ExaminationType.Ok);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            _examinationType = ExaminationType.Ng;
            OnExamination?.Invoke(ExaminationType.Ng);
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _examinationType = ExaminationType.Blockade;
            OnExamination?.Invoke(ExaminationType.Blockade);
        }
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
