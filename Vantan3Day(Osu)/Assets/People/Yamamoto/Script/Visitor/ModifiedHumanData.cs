using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 改造人間
/// </summary>
[CreateAssetMenu(menuName = "ModifiedHumanData")]
public class ModifiedHumanData : VisitorBaseData
{
    public override async UniTask Visit(VisitorManager manager)
    {
        _visitorManager = manager;
        _visitorManager.VisitorAwaitSet();
        _visitorManager.OnEntry?.Invoke();
    }
    
    public override async UniTask ExaminationOk()
    {
        
    }

    public override async UniTask ExaminationNg()
    {
        
    }

    public override async UniTask ExaminationNeglect()
    {
        
    }

    public override async UniTask ExaminationBlockade()
    {
        
    }

    public override void Exit()
    {
        _visitorManager.OnExit?.Invoke();
    }
}
