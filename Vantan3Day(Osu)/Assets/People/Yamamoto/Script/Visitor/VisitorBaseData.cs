using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 来訪者のベースデータ
/// </summary>
public class VisitorBaseData : ScriptableObject
{
    [Header("来訪者の見た目")]
    [SerializeField] private Sprite _sprite;
    [Header("来訪者の種類")]
    [SerializeField] private VisitorType _visitorType;
    [Header("来訪者の顔差分")]
    [SerializeField] private List<FaceVariationsInfo> _sprites;
    public List<FaceVariationsInfo> FaceVariations => _sprites;
    public Sprite Sprite => _sprite;
    
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

    /// <summary>
    /// 差分を取得する
    /// </summary>
    /// <returns>差分を返す</returns>
    /// <param name="type">顔差分の種類</param>>
    public virtual Sprite GetFaceVariations(FaceVariationsType type)
    {
        return null;
    }
}
