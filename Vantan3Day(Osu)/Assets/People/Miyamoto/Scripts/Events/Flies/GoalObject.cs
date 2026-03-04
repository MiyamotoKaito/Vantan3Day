using UnityEngine;

public abstract class GoalObject : MonoBehaviour
{
    public virtual Vector2 Position
    {
        get => _pos;
        protected set => _pos = value;
    }

    protected Vector2 _pos;
}

