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

//TODO：ここに妨害処理を追加していく
