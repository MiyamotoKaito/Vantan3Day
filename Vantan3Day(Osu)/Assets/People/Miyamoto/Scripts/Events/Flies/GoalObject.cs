using UnityEngine;

public abstract class GoalObject : MonoBehaviour
{
    public Vector2 Position => _pos;
    protected Vector2 _pos;
    public bool IsActiveObject => _isActiveObject;
    protected bool _isActiveObject;
}