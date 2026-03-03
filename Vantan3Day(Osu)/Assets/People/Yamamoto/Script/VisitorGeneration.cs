using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 来訪者の生成を行う
/// 人間、宇宙人、改造人間をランダムで来訪させる
/// </summary>
public class VisitorGeneration : MonoBehaviour
{
    [Header("来訪者の種類")]
    [SerializeField] private List<VisitorBaseData> _visitors;

    /// <summary>
    /// 現在の来訪者を保持
    /// </summary>
    public VisitorBaseData CurrentVisitor { get; private set; }
}
