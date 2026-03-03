using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 宇宙人
/// </summary>
[CreateAssetMenu(menuName = "AliensData")]
public class AliensVisitorData : VisitorBaseData
{
    public override async UniTask Visit(VisitorManager manager)
    {
        _visitorManager = manager;
        _visitorManager.VisitorAwaitSet();
        await UniTask.Delay(1000);
        _visitorManager.OnEntry?.Invoke();
        await UniTask.Delay(2000);
        _visitorManager.SetInput(true);
    }
    
    public override async UniTask ExaminationOk()
    {
        Debug.LogWarning("妨害後、通過");
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
        await UniTask.Delay(2000);
        _visitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationNg()
    {
        Debug.LogWarning("立ち去る");
    }

    public override async UniTask ExaminationNeglect()
    {
        Debug.LogWarning("妨害実行");
    }

    public override async UniTask ExaminationBlockade()
    {
        Debug.LogWarning("封鎖後、妨害を実行");
    }
    
    public override void Exit()
    {
        _visitorManager.OnExit?.Invoke();
    }
}
