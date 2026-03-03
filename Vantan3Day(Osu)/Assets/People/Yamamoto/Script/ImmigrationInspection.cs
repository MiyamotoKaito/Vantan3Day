using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 入国審査
/// </summary>
public class ImmigrationInspection : MonoBehaviour
{
    [Header("VisitorManager")] 
    [SerializeField] private VisitorManager _visitorManager;
    
    /// <summary>
    /// 入力した審査の結果
    /// </summary>
    private ExaminationType _examinationType;

    private void Update()
    {
        ReviewInput();
    }

    /// <summary>
    /// 審査の入力
    /// </summary>
    private void ReviewInput()
    {
        //TODO：Q：OK　W：NG　E：放置　R：封鎖
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            _examinationType = ExaminationType.Ok;
            //Debug.LogWarning(_examinationType + "OK");
            ExaminationJudgment();
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            _examinationType = ExaminationType.Ng;
            //Debug.LogWarning(_examinationType + "NG");
            ExaminationJudgment();
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _examinationType = ExaminationType.Neglect;
            //Debug.LogWarning(_examinationType + "放置");
            ExaminationJudgment();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _examinationType = ExaminationType.Blockade;
            //Debug.LogWarning(_examinationType + "封鎖");
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
                _visitorManager.onVisitor?.Invoke();
                break;
            case ExaminationType.Ng:
                visitorData.ExaminationNg();
                _visitorManager.onVisitor?.Invoke();
                break;
            case ExaminationType.Neglect:
                visitorData.ExaminationNeglect();
                _visitorManager.onVisitor?.Invoke();
                break;
            case ExaminationType.Blockade:
                visitorData.ExaminationBlockade();
                _visitorManager.onVisitor?.Invoke();
                break;
        }
    }
}
