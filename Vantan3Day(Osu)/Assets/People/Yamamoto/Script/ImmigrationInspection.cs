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
        if(!_visitorManager.IsExaminationInput) return;
        //TODO：Q：OK　W：NG　E：放置　R：封鎖
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

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _examinationType = ExaminationType.Neglect;
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
