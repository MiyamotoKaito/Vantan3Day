using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 入国審査
/// </summary>
public class ImmigrationInspection : MonoBehaviour
{
    [Header("VisitorManager")] 
    [SerializeField] private VisitorManager _visitorManager;

    [Header("放置時間")]
    [SerializeField] private float _neglectTime;
    private float _neglectTimer; //放置時間のタイマー

    [Header("放置時間のデバッグUI")] 
    [SerializeField] private TextMeshProUGUI _neglectText;
    
    /// <summary>
    /// 入力した審査の結果
    /// </summary>
    private ExaminationType _examinationType;
    
    private void Awake()
    {
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
            //TODO：ここでゲームオーバー処理を行う
            
            
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
        if(!_visitorManager.IsExaminationInput) return;
        //TODO：Q：OK　W：NG　R：封鎖
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            _examinationType = ExaminationType.Ok;
            ExaminationJudgment();
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            _examinationType = ExaminationType.Ng;
            ExaminationJudgment();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _examinationType = ExaminationType.Blockade;
            ExaminationJudgment();
        }
    }
    
    /// <summary>
    /// 審査の判定を行う
    /// </summary>
    private void ExaminationJudgment()
    {
        //入力した審査
        var visitorData = _visitorManager.CurrentVisitor;
        switch (_examinationType)
        {
            case ExaminationType.Ok:
                visitorData.ExaminationOk();
                break;
            case ExaminationType.Ng:
                visitorData.ExaminationNg();
                break;
            case ExaminationType.Neglect:
                visitorData.ExaminationNeglect();
                break;
            case ExaminationType.Blockade:
                visitorData.ExaminationBlockade();
                break;
        }
    }
}
