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
//TODO：ここで全ての妨害処理を書いていく
//TODO：宇宙人などの妨害

/// <summary>
/// 視界がぼやける
/// </summary>
public class VisionBecomeBlurry : Obstruction
{
    public override void ObstructionExecution()
    {
        
    }
}

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
