using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 宇宙人
/// </summary>
[CreateAssetMenu(menuName = "AliensData")]
public class AliensVisitorData : VisitorBaseData
{
    public override async UniTask Visit(VisitorManager manager, float time)
    {
        VisitorManager = manager;
        AwaitTime = time;
        VisitorManager.VisitorAwaitSet();
        await UniTask.Delay(1000);
        VisitorManager.OnEntry?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.SetInput(true);
    }
    
    public override async UniTask ExaminationOk()
    {
        Debug.LogWarning("妨害後、通過");
        VisitorManager.OnExit?.Invoke();
        VisitorManager.SetInput(false);
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationNg()
    {
        Debug.LogWarning("立ち去る");
        VisitorManager.OnExit?.Invoke();
        VisitorManager.SetInput(false);
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationNeglect()
    {
        Debug.LogWarning("妨害実行");
        VisitorManager.OnExit?.Invoke();
        VisitorManager.SetInput(false);
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationBlockade()
    {
        Debug.LogWarning("封鎖後、妨害を実行");
        VisitorManager.OnExit?.Invoke();
        VisitorManager.SetInput(false);
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }
}
