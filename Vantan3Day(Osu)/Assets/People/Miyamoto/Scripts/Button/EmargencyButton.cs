using UnityEngine;
using UnityEngine.EventSystems;

public class EmargencyButton : GoalObject, IPointerClickHandler
{
    private Animator _animator;

    public void OnPointerClick(PointerEventData eventData)
    {
        // GAMEOVER
        Debug.Log("GAMEOVER");
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _pos = this.transform.position;
    }
}
