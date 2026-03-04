using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

/// <summary>
/// 来訪者がプレイヤーに物をぶつける処理
/// </summary>
public class VisitorThrowAt : MonoBehaviour
{
    [Header("VisitorManager")]
    [SerializeField] private VisitorManager _visitorManager;
    [Header("UI関連")] 
    [Header("生成場所")] 
    [SerializeField] private Transform _genePos;
    [Header("物の種類（UIPrefab）")]
    [SerializeField] private List<GameObject> _thing;

    [Header("物の設定")] 
    [Header("アニメーション秒数")] 
    [SerializeField] private float _animTime;
    [Header("拡大")] 
    [SerializeField] private Vector3 _enlargement;

    private GameObject _throwObj;

    private void Awake()
    {
        _visitorManager.OnThingThrow += () =>
        {
            ThingGeneration();
            ToLaunch();
        };
    }

    /// <summary>
    /// 物を生成
    /// </summary>
    private void ThingGeneration()
    {
        var random = Random.Range(0, _thing.Count);
        var obj = _thing[random];
        _throwObj = Instantiate(obj,  _genePos);
        Destroy(_throwObj, _animTime);
    }

    /// <summary>
    /// 物を飛ばす
    /// </summary>
    private void ToLaunch()
    {
        var sq = DOTween.Sequence();
        sq.Append(_throwObj.transform.DOScale(_enlargement, _animTime))
            .Join(_throwObj.transform.DORotate(new Vector3(0, 0, 180), _animTime, RotateMode.WorldAxisAdd));
    }
}
