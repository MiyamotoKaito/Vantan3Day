using UnityEngine;

/// <summary>
/// 来訪者のベースデータ
/// </summary>
public class VisitorBaseData : ScriptableObject
{
    [Header("来訪者の見た目")]
    [SerializeField] private Sprite _sprite;
    [Header("来訪者の種類")]
    [SerializeField] private VisitorType _visitorType;
    
    public Sprite Sprite => _sprite;
    /// <summary>
    /// 来訪者の種類
    /// </summary>
    public VisitorType VisitorType => _visitorType;
    
    public VisitorManager _visitorManager;
    
    //TODO：ここをUniTaskに変更し、退場処理を待機させ終了後、入国処理をおこなう
    
    /// <summary>
    /// 来訪
    /// </summary>
    public virtual void Visit(VisitorManager manager){}

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
    
    public virtual void Exit(){}
}
