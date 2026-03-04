using UnityEngine;

/// <summary>
/// 審査の種類
/// </summary>
public enum ExaminationType
{
    [InspectorName("OK印")]
    Ok,
    [InspectorName("NG印")]
    Ng,
    [InspectorName("封鎖")]
    Blockade
}
