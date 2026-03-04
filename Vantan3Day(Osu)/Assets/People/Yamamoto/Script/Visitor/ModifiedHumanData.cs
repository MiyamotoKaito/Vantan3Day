using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 改造人間
/// </summary>
[CreateAssetMenu(menuName = "ModifiedHumanData")]
public class ModifiedHumanData : VisitorBaseData
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
        //TODO：妨害実行
        var ob = new CompulsoryGameOver();
        ob.ObstructionExecution();
        VisitorManager.SetNeglectTimeFlag(false);
    }

    public override async UniTask ExaminationNg()
    {
        //TODO：妨害実行
        var ob = new CompulsoryGameOver();
        ob.ObstructionExecution();
        VisitorManager.SetNeglectTimeFlag(false);
    }

    public override async UniTask ExaminationNeglect()
    {
        //TODO：妨害実行
        var ob = new CompulsoryGameOver();
        ob.ObstructionExecution();
        VisitorManager.SetNeglectTimeFlag(false);
    }

    public override async UniTask ExaminationBlockade()
    {
        Debug.LogWarning("立ち去る");
        VisitorManager.SetNeglectTimeFlag(false);
        VisitorManager.SetInput(false);
        VisitorManager.OnLeave?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }
}
