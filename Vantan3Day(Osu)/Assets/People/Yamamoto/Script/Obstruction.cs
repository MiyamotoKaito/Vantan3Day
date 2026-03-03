/// <summary>
/// 妨害
/// </summary>
public class Obstruction
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

//TODO：ここに妨害処理を追加していく
