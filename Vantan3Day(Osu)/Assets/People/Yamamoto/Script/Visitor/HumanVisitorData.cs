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
    
    public override void ExaminationOk()
    {
        
    }

    public override void ExaminationNg()
    {
        
    }

    public override void ExaminationNeglect()
    {
        
    }

    public override void ExaminationBlockade()
    {
        
    }
}
