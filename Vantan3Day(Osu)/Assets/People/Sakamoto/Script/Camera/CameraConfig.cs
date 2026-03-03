using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "ScriptableObjects/CameraConfig", order = 0)]
public class CameraConfig : ScriptableObject
{
    public float XMin => _xMin;
    public float XMax => _xMax;
    public float XMoveSpeed => _xMoveSpeed;


    [SerializeField] private float _xMin;
    [SerializeField] private float _xMax;
    [SerializeField] private float _xMoveSpeed;
}
