using UnityEngine;

/// <summary>
/// 人間
/// </summary>
[CreateAssetMenu(menuName = "HumanData")]
public class HumanVisitorData : VisitorBaseData
{
    [Header("視界がぼやける時間")]
    [SerializeField] private float _becomeBlurryTime;
    public float BecomeBlurryTime => _becomeBlurryTime;
    
    //TODO：基本的に、SetInputの後にOnExitを呼ぶ
    
    public override void Visit(VisitorManager manager)
    {
        _visitorManager = manager;
        _visitorManager.VisitorAwaitSet();
        _visitorManager.OnEntry?.Invoke();
        _visitorManager.SetInput(true);
    }
    
    public override void ExaminationOk()
    {
        Debug.LogWarning("通過");
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
        //_visitorManager.OnVisitor?.Invoke();
    }

    public override void ExaminationNg()
    {
        Debug.LogWarning("NGで物をぶつける");
        _visitorManager.OnThingThrow?.Invoke();
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
    }

    public override void ExaminationNeglect()
    {
        Debug.LogWarning("放置で物をぶつける");
        _visitorManager.OnThingThrow?.Invoke();
        _visitorManager.OnThingThrow?.Invoke();
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
    }

    public override void ExaminationBlockade()
    {
        Debug.LogWarning("封鎖で物をぶつける");
        _visitorManager.OnThingThrow?.Invoke();
        _visitorManager.OnThingThrow?.Invoke();
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
    }
    
    public override void Exit()
    {
        _visitorManager.OnExit?.Invoke();
        _visitorManager.SetInput(false);
    }
}
