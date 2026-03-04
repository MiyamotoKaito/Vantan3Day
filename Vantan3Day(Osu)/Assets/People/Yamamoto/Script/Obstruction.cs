/// <summary>
/// 妨害
/// </summary>
public abstract class Obstruction
{
    /// <summary>
    /// 妨害実行
    /// </summary>
    public virtual void ObstructionExecution(){}
}

//TODO：一旦、ここは無しで妨害は実装しないようにする
//TODO：ゲームオーバー以外、妨害なし

/// <summary>
/// 強制的にゲームオーバー
/// </summary>
public class CompulsoryGameOver : Obstruction
{
    public override void ObstructionExecution()
    {
        InGameManager.Instance.OnGameOver?.Invoke();
    }
}
