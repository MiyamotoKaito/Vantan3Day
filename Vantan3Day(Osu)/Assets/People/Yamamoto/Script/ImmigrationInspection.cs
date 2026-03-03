using UnityEngine;

/// <summary>
/// 入国審査
/// </summary>
public class ImmigrationInspection : MonoBehaviour
{
    [Header("VisitorGeneration")] 
    [SerializeField] private VisitorGeneration _visitorGeneration;
    
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
        //TODO：仮の入力を実装
        //TODO：のちに、InputSystemで対応させる
        //TODO：Q：OK　W：NG　E：放置　R：封鎖
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _examinationType = ExaminationType.Ok;
            Debug.LogWarning(_examinationType + "OK");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _examinationType = ExaminationType.Ng;
            Debug.LogWarning(_examinationType + "NG");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _examinationType = ExaminationType.Neglect;
            Debug.LogWarning(_examinationType + "放置");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _examinationType = ExaminationType.Blockade;
            Debug.LogWarning(_examinationType + "封鎖");
        }
    }
    
    /// <summary>
    /// 審査の判定を行う
    /// </summary>
    private void ExaminationJudgment()
    {
        /*
        var data = _visitorGeneration.CurrentVisitor.VisitorType;
        switch (data)
        {
            case VisitorType.Human:
                break;
            case VisitorType.Alien:
                break;
            case VisitorType.ArtificialHuman:
                break;
        }
        */
        
        //TODO：ここで入力に応じた審査処理を来訪者データから呼び出す
    }
}
