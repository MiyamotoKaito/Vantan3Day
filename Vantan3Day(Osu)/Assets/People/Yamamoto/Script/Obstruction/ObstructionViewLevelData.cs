using UnityEngine;

/// <summary>
/// 視界妨害のレベル
/// </summary>
[CreateAssetMenu(fileName = "ObstructionViewLevelData", menuName = "ScriptableObjects/ObstructionViewLevelData")]
public class ObstructionViewLevelData : ScriptableObject
{
    [Header("レベル１")]
    [Range(0, 100)]
    [SerializeField] private int _level1;
    [Header("レベル２")]
    [Range(0, 100)]
    [SerializeField] private int _level2;
    [Header("レベル３")]
    [Range(0, 100)]
    [SerializeField] private int _level3;
    [Header("レベル４")]
    [Range(0, 100)]
    [SerializeField] private int _level4;
    [Header("レベル５")]
    [Range(0, 100)]
    [SerializeField] private int _level5;
    
    public int Level1 => _level1;
    public int Level2 => _level2;
    public int Level3 => _level3;
    public int Level4 => _level4;
    public int Level5 => _level5;
}
