using UnityEngine;

/// <summary>
/// 顔差分の種類
/// </summary>
public enum FaceVariationsType
{
    [InspectorName("通常")]
    Normal,
    [InspectorName("怒り")]
    Anger
}

/// <summary>
/// 来訪者の顔差分の情報
/// </summary>
[System.Serializable]
public class FaceVariationsInfo
{
    [Header("顔差分の種類")]
    [SerializeField] private FaceVariationsType _type;
    [Header("顔差分")]
    [SerializeField] private Sprite _sprite;
    
    public FaceVariationsType Type => _type;
    public Sprite Sprite => _sprite;
}
