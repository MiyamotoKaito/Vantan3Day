using UnityEngine;
using UnityEngine.EventSystems;

public class EmargencyButton : GoalObject, IPointerClickHandler
{
    private Animator _animator;
    private InGameManager _gameManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        // GAMEOVER
        Debug.Log("GAMEOVER");
        _gameManager.OnGameOver();
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _pos = this.transform.position;
        _gameManager = FindAnyObjectByType<InGameManager>();
    }
}
