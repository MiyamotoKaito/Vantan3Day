using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig", order = 0)]
public class PlayerConfig : ScriptableObject
{
    public float RightHandMinX => _rightHandMinX;
    public float RightHandMaxX => _rightHandMaxX;
    public float LeftHandMinX => _leftHandMinX;
    public float LeftHandMaxX => _leftHandMaxX;
    public float HandMinY => _handMinY;
    public float HandMaxY => _handMaxY;

    [SerializeField] private float _rightHandMinX;
    [SerializeField] private float _rightHandMaxX;
    [SerializeField] private float _leftHandMinX;
    [SerializeField] private float _leftHandMaxX;
    [SerializeField] private float _handMinY;
    [SerializeField] private float _handMaxY;
}
