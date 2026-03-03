using UnityEngine;

/// <summary>
/// 来訪者のベースデータ
/// </summary>
public class VisitorBaseData : ScriptableObject
{
    [Header("来訪者の種類")]
    [SerializeField] private VisitorType _visitorType;
    
    /// <summary>
    /// 来訪者の種類
    /// </summary>
    public VisitorType VisitorType => _visitorType;

    /// <summary>
    /// OK
    /// </summary>
    public virtual void ExaminationOk(){}
    
    /// <summary>
    /// NG
    /// </summary>
    public virtual void ExaminationNg(){}
    
    /// <summary>
    /// 放置
    /// </summary>
    public virtual void ExaminationNeglect(){}
    
    /// <summary>
    /// 封鎖
    /// </summary>
    public virtual void ExaminationBlockade(){}
}
