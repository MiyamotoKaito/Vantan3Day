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
}
