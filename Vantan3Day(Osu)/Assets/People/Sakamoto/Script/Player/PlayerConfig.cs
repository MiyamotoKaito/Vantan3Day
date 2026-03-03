using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig", order = 0)]
public class PlayerConfig : ScriptableObject
{
    public float ArmMinX => _armMinX;
    public float ArmMinY => _armMinY;
    public float ArmMaxX => _armMaxX;
    public float ArmMaxY => _armMaxY;

    [SerializeField] private float _armMinX;
    [SerializeField] private float _armMinY;
    [SerializeField] private float _armMaxX;
    [SerializeField] private float _armMaxY;
}
