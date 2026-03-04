using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 改造人間
/// </summary>
[CreateAssetMenu(menuName = "ModifiedHumanData")]
public class ModifiedHumanData : VisitorBaseData
{
    [Header("人造人間のアニメーション時間")] 
    [SerializeField] private float _animTime;
    
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
        VisitorManager.SetNeglectTimeFlag(false);
        await UniTask.Delay(TimeSpan.FromSeconds(_animTime));
        var ob = new CompulsoryGameOver();
        ob.ObstructionExecution();
    }

    public override async UniTask ExaminationNg()
    {
        VisitorManager.SetNeglectTimeFlag(false);
        await UniTask.Delay(TimeSpan.FromSeconds(_animTime));
        var ob = new CompulsoryGameOver();
        ob.ObstructionExecution();
    }

    public override async UniTask ExaminationNeglect()
    {
        VisitorManager.SetNeglectTimeFlag(false);
        await UniTask.Delay(TimeSpan.FromSeconds(_animTime));
        var ob = new CompulsoryGameOver();
        ob.ObstructionExecution();
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
