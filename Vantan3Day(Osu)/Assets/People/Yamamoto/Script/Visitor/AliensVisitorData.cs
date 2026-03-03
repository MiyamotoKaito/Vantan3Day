using UnityEngine;

/// <summary>
/// 宇宙人
/// </summary>
[CreateAssetMenu(menuName = "AliensData")]
public class AliensVisitorData : VisitorBaseData
{
    public override void Visit()
    {
        
    }
    
    public override void ExaminationOk()
    {
        Debug.LogWarning("妨害後、通過");
    }

    public override void ExaminationNg()
    {
        Debug.LogWarning("立ち去る");
    }

    public override void ExaminationNeglect()
    {
        Debug.LogWarning("妨害実行");
    }

    public override void ExaminationBlockade()
    {
        Debug.LogWarning("封鎖後、妨害を実行");
    }
}
