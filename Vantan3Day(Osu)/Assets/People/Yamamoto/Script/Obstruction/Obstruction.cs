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
/// 視界をぼやけさせる
/// </summary>
public class BecomeBlurry : Obstruction
{
    private float _time;
    private ObstructionViewLevel _level;
    
    public BecomeBlurry(float time, ObstructionViewLevel level)
    {
        _time = time;
        _level = level;
    }
    
    public override void ObstructionExecution()
    {
        BecomeBlurryManager.Instance.OnBecomeBlurry?.Invoke(_time, _level);
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
