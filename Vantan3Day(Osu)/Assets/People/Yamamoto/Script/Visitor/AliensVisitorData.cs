using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 宇宙人
/// </summary>
[CreateAssetMenu(menuName = "AliensData")]
public class AliensVisitorData : VisitorBaseData
{
    public override async UniTask Visit(VisitorManager manager, float time)
    {
        VisitorManager = manager;
        AwaitTime = time;
        VisitorManager.VisitorAwaitSet();
        await UniTask.Delay(1000);
        VisitorManager.OnEntry?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.SetInput(true);
        VisitorManager.OnVisitorInfoCard?.Invoke(true);
    }
    
    public override async UniTask ExaminationOk()
    {
        if(!VisitorManager.IsExaminationInput)return;
        Debug.LogWarning("妨害後、通過");
        VisitorManager.SetNeglectTimeFlag(false);
        VisitorManager.SetInput(false);
        VisitorManager.VisitorFaceChange(GetFaceVariations(FaceVariationsType.Anger));
        VisitorManager.OnBattely?.Invoke();
        VisitorManager.OnVisitorInfoCard?.Invoke(false);
        VisitorManager.OnExit?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationNg()
    {
        if(!VisitorManager.IsExaminationInput)return;
        Debug.LogWarning("立ち去る");
        VisitorManager.SetNeglectTimeFlag(false);
        VisitorManager.SetInput(false);
        VisitorManager.OnVisitorInfoCard?.Invoke(false);
        VisitorManager.OnGoBack?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationNeglect()
    {
        if(!VisitorManager.IsExaminationInput)return;
        Debug.LogWarning("妨害実行");
        VisitorManager.SetNeglectTimeFlag(false);
        VisitorManager.SetInput(false);
        VisitorManager.VisitorFaceChange(GetFaceVariations(FaceVariationsType.Anger));
        VisitorManager.OnBattely?.Invoke();
        VisitorManager.OnVisitorInfoCard?.Invoke(false);
        VisitorManager.OnGoBack?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override async UniTask ExaminationBlockade()
    {
        if(!VisitorManager.IsExaminationInput)return;
        Debug.LogWarning("封鎖後、妨害を実行");
        VisitorManager.SetNeglectTimeFlag(false);
        VisitorManager.SetInput(false);
        VisitorManager.VisitorFaceChange(GetFaceVariations(FaceVariationsType.Anger));
        VisitorManager.OnVisitorInfoCard?.Invoke(false);
        VisitorManager.OnGoBack?.Invoke();
        await UniTask.Delay(TimeSpan.FromSeconds(AwaitTime));
        VisitorManager.OnVisitor?.Invoke();
    }

    public override Sprite GetFaceVariations(FaceVariationsType type)
    {
        Sprite sp = null;
        foreach (var face in FaceVariations)
        { 
            if(face.Type == type) sp = face.Sprite;
        }

        return sp;
    }
}
