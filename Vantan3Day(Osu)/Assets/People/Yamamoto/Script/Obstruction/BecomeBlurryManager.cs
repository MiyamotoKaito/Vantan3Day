using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 視界をぼやけさせる
/// ImageのAを変更など
/// </summary>
public class BecomeBlurryManager : MonoBehaviour
{
    public static BecomeBlurryManager Instance;

    [Header("視界妨害のレベルデータ")] 
    [SerializeField] private ObstructionViewLevelData _data;
    [Header("視界")]
    [SerializeField] private Image _visionImage;
    [Header("アニメーション時間")] 
    [SerializeField] private float _animTime;

    /// <summary>
    /// 視界妨害
    /// float：妨害時間
    /// ObstructionViewLevel：レベル
    /// </summary>
    public Action<float, ObstructionViewLevel> OnBecomeBlurry;
    
    /// <summary>
    /// 視界回復時間のタイマー
    /// </summary>
    private float _recoveryTimer;
    /// <summary>
    /// 視界妨害が開始
    /// true：開始　false：終止
    /// </summary>
    private bool _isObstructionView;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        OnBecomeBlurry += SetBecomeBlurry;
    }

    private void Update()
    {
        //視界妨害を開始する
        if (_isObstructionView)
        {
            _recoveryTimer -= Time.deltaTime;
            if (_recoveryTimer <= 0) //妨害時間が終了したら、視界を元に戻す
            {
                _recoveryTimer = 0;
                _isObstructionView = false;
                
                //透明度を元に戻す
                var color = _visionImage.color;
                color.a = 0;
                _visionImage.DOColor(color, _animTime);
            }
        }
    }

    /// <summary>
    /// 視界妨害を設定
    /// </summary>
    /// <param name="time">妨害時間</param>>
    /// <param name="level">妨害のレベル</param>>
    private void SetBecomeBlurry(float time, ObstructionViewLevel level)
    {
        float alpha = 0;
        var color = _visionImage.color;
        switch (level)
        {
            case ObstructionViewLevel.Level1:
                alpha = _data.Level1;
                break;
            case ObstructionViewLevel.Level2:
                alpha = _data.Level2;
                break;
            case ObstructionViewLevel.Level3:
                alpha = _data.Level3;
                break;
            case ObstructionViewLevel.Level4:
                alpha = _data.Level4;
                break;
            case ObstructionViewLevel.Level5:
                alpha = _data.Level5;
                break;
        }

        //透明度を変更
        float alphaColor = alpha / 100;
        color.a = alphaColor;
        _visionImage.DOColor(color, _animTime);
        
        _recoveryTimer += time;
        _isObstructionView = true;
    }
}
