using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 人間
/// </summary>
[CreateAssetMenu(menuName = "HumanData")]
public class HumanVisitorData : VisitorBaseData
{
    [Header("視界がぼやける時間")]
    [SerializeField] private float _becomeBlurryTime;
    public float BecomeBlurryTime => _becomeBlurryTime;
    
    //TODO：このDelayをインスペクターで設定できるようにする
    
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
        Debug.LogWarning("通過");
        VisitorManager.OnExit?.Invoke();
        VisitorManager.SetInput(false);
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationNg()
    {
        Debug.LogWarning("NGで物をぶつける");
        VisitorManager.OnThingThrow?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnExit?.Invoke();
        VisitorManager.SetInput(false);
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationNeglect()
    {
        Debug.LogWarning("放置で物をぶつける");
        VisitorManager.OnThingThrow?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnExit?.Invoke();
        VisitorManager.SetInput(false);
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationBlockade()
    {
        Debug.LogWarning("封鎖で物をぶつける");
        VisitorManager.OnThingThrow?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnExit?.Invoke();
        VisitorManager.SetInput(false);
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }
    
    public override void Exit()
    {
        VisitorManager.OnExit?.Invoke();
        VisitorManager.SetInput(false);
    }
}
