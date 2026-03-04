using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmargencyButton : GoalObject, IPointerClickHandler
{
    private Animator _animator;
    private PlayerController _player;
    private bool _isPush;
    public void OnPointerClick(PointerEventData eventData)
    {
        // GAMEOVER
        Debug.Log("GAMEOVER");
        ButtonPush().Forget();
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _pos = this.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 当たったオブジェクトがアイテム且つY軸が上だったらボタンを押せるようにする
        if (other.TryGetComponent<Item>(out var item) && item.transform.position.y > transform.position.y)
        {
            Debug.Log("GAMEOVER");
            ButtonPush().Forget();
        }
    }
    /// <summary>
    /// ボタンを押す
    /// </summary>
    /// <returns></returns>
    public async UniTask ButtonPush()
    {
        if (_isPush) return;
        _animator.SetTrigger("Push");
        _isPush = true;
        await UniTask.NextFrame();
        await UniTask.WaitUntil(() =>
                                // BaseLayerのアニメーションが終わるまで待つ
                                _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
                                // 他のアニメーションに遷移していないか確認
                                && !_animator.IsInTransition(0));

        InGameManager.Instance.OnGameOver.Invoke();
    }
}
