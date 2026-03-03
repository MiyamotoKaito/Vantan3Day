using UnityEngine;

/// <summary>
/// 来訪者の種類
/// </summary>
public enum VisitorType
{
    [InspectorName("人間")]
    Human,
    [InspectorName("宇宙人")]
    Alien,
    [InspectorName("人造人間")]
    ArtificialHuman
}
