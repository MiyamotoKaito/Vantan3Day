using UnityEngine;

/// <summary>
/// 宇宙人
/// </summary>
[CreateAssetMenu(menuName = "AliensData")]
public class AliensVisitorData : VisitorBaseData
{
    public override void Visit(VisitorManager manager)
    {
        _visitorManager = manager;
        _visitorManager.VisitorAwaitSet();
        _visitorManager.OnEntry?.Invoke();
        _visitorManager.SetInput(true);
    }
    
    public override void ExaminationOk()
    {
        Debug.LogWarning("妨害後、通過");
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
        //_visitorManager.OnVisitor?.Invoke();
    }

    public override void ExaminationNg()
    {
        Debug.LogWarning("立ち去る");
    }

    public override void ExaminationNeglect()
    {
        Debug.LogWarning("妨害実行");
    }

    public override void ExaminationBlockade()
    {
        Debug.LogWarning("封鎖後、妨害を実行");
    }
    
    public override void Exit()
    {
        _visitorManager.OnExit?.Invoke();
    }
}
