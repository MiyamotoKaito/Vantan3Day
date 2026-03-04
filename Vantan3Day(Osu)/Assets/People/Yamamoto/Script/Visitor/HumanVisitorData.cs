using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 人間
/// </summary>
[CreateAssetMenu(menuName = "HumanData")]
public class HumanVisitorData : VisitorBaseData
{
    [Header("視界妨害のレベル")] 
    [SerializeField] private ObstructionViewLevel _level;
    [Header("視界妨害時間")]
    [SerializeField] private float _obstructionViewTime;
    
    public override async UniTask Visit(VisitorManager manager, float time)
    {
        VisitorManager = manager;
        AwaitTime = time;
        VisitorManager.VisitorAwaitSet();
        await UniTask.Delay(1000);
        VisitorManager.OnEntry?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.SetInput(true);
        VisitorManager.OnVisitorInfoCard?.Invoke(true);
    }

    public override async UniTask ExaminationOk()
    {
        if(!VisitorManager.IsExaminationInput)return;
        Debug.LogWarning("通過");
        VisitorManager.SetNeglectTimeFlag(false);
        VisitorManager.SetInput(false);
        VisitorManager.OnVisitorInfoCard?.Invoke(false);
        VisitorManager.OnExit?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationNg()
    {
        if(!VisitorManager.IsExaminationInput)return;
        Debug.LogWarning("NGで物をぶつける");
        VisitorManager.SetNeglectTimeFlag(false);
        VisitorManager.SetInput(false);
        var ob = new BecomeBlurry(_obstructionViewTime, _level);
        ob.ObstructionExecution();
        VisitorManager.OnVisitorInfoCard?.Invoke(false);
        VisitorManager.OnThingThrow?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnGoBack?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationNeglect()
    {
        if(!VisitorManager.IsExaminationInput)return;
        Debug.LogWarning("放置で物をぶつける");
        VisitorManager.SetNeglectTimeFlag(false);
        VisitorManager.SetInput(false);
        var ob = new BecomeBlurry(_obstructionViewTime, _level);
        ob.ObstructionExecution();
        VisitorManager.OnVisitorInfoCard?.Invoke(false);
        VisitorManager.OnThingThrow?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnGoBack?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationBlockade()
    {
        if(!VisitorManager.IsExaminationInput)return;
        Debug.LogWarning("封鎖で物をぶつける");
        VisitorManager.SetNeglectTimeFlag(false);
        VisitorManager.SetInput(false);
        var ob = new BecomeBlurry(_obstructionViewTime, _level);
        ob.ObstructionExecution();
        VisitorManager.OnVisitorInfoCard?.Invoke(false);
        VisitorManager.OnThingThrow?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnGoBack?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override Sprite GetFaceVariations(FaceVariationsType type)
    {
        Sprite sp = null;
        foreach (var face in FaceVariations)
        { 
            if(face.Type == type) sp = face.Sprite;
        }

        return sp;
    }
}
