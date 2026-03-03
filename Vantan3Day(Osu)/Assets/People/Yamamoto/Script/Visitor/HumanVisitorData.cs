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
        Debug.LogWarning("通過");
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
        await UniTask.Delay(2000);
        _visitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationNg()
    {
        Debug.LogWarning("NGで物をぶつける");
        _visitorManager.OnThingThrow?.Invoke();
        await UniTask.Delay(2000);
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
        await UniTask.Delay(2000);
        _visitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationNeglect()
    {
        Debug.LogWarning("放置で物をぶつける");
        _visitorManager.OnThingThrow?.Invoke();
        await UniTask.Delay(2000);
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
        await UniTask.Delay(2000);
        _visitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationBlockade()
    {
        Debug.LogWarning("封鎖で物をぶつける");
        _visitorManager.OnThingThrow?.Invoke();
        await UniTask.Delay(2000);
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
        await UniTask.Delay(2000);
        _visitorManager.OnVisitor?.Invoke();
    }
    
    public override void Exit()
    {
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
    }
}
