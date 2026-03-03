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
    
    public override void Visit()
    {
        
    }
    
    public override void ExaminationOk()
    {
        Debug.LogWarning("通過");
    }

    public override void ExaminationNg()
    {
        Debug.LogWarning("NGで物をぶつける");
    }

    public override void ExaminationNeglect()
    {
        Debug.LogWarning("放置で物をぶつける");
    }

    public override void ExaminationBlockade()
    {
        Debug.LogWarning("封鎖で物をぶつける");
    }
}
