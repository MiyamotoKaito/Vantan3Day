using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Serialization;

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
    
    [HideInInspector] public VisitorManager VisitorManager;
    [HideInInspector] public float AwaitTime;
    
    /// <summary>
    /// 来訪
    /// </summary>
    /// <param name="manager">VisitorManager：来訪者管理</param>>
    /// <param name="time">待機時間</param>>
    public virtual async UniTask Visit(VisitorManager manager, float time){}

    /// <summary>
    /// OK
    /// </summary>
    public virtual async UniTask ExaminationOk(){}
    
    /// <summary>
    /// NG
    /// </summary>
    public virtual async UniTask ExaminationNg(){}
    
    /// <summary>
    /// 放置
    /// </summary>
    public virtual async UniTask ExaminationNeglect(){}
    
    /// <summary>
    /// 封鎖
    /// </summary>
    public virtual async UniTask ExaminationBlockade(){}
    
    public virtual void Exit(){}
}
