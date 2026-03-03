using UnityEngine;
/// <summary>
/// 改造人間
/// </summary>
[CreateAssetMenu(menuName = "ModifiedHumanData")]
public class ModifiedHumanData : VisitorBaseData
{
    public override void Visit(VisitorManager manager)
    {
        _visitorManager = manager;
        _visitorManager.VisitorAwaitSet();
        _visitorManager.OnEntry?.Invoke();
    }
    
    public override void ExaminationOk()
    {
        
    }

    public override void ExaminationNg()
    {
        
    }

    public override void ExaminationNeglect()
    {
        
    }

    public override void ExaminationBlockade()
    {
        
    }

    public override void Exit()
    {
        _visitorManager.OnExit?.Invoke();
    }
}
