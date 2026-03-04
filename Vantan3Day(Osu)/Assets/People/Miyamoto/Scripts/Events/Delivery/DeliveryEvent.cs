using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
/// <summary>
///　配達イベント
/// </summary>
[Serializable]
public class DeliveryEvent : IEvent
{
    /// <summary>
    /// 配達場所のクラス
    /// </summary>
    [System.Serializable]
    public class ProviderPos
    {
        public Vector3 StartPos => _startPos;
        public Vector3 EndPos => _endPos;
        public bool IsDelivered => _isDelivered;
        /// <summary>配達開始場所</summary>
        [SerializeField]
        private Vector3 _startPos;
        /// <summary>配達場所</summary>
        [SerializeField]
        private Vector3 _endPos;
        /// <summary>既に配達済み</summary>
        private bool _isDelivered;

        public void IsDelivery(bool flag)
        {
            _isDelivered = flag;
        }
    }

    [SerializeField]
    private List<GameObject> _providerList;
    [SerializeField]
    private float _deliverySpeed;
    [SerializeField]
    private List<ProviderPos> _posList;

    private List<BaseDeliveryItem> _itemList = new();
    private Dictionary<BaseDeliveryItem, Action> _handlerMap = new();
    public void OnEvent(EventManager eventManager)
    {
        OnDelivery();
    }
    private void OnDelivery()
    {
        var available = _posList.Where(p => !p.IsDelivered).ToList();

        // 配達可能な場所がなければ早期リターン
        if (available.Count == 0) return;

        var selected = available[UnityEngine.Random.Range(0, available.Count)];

        // 配達中フラグをTrueに
        selected.IsDelivery(true);

        var obj = UnityEngine.Object.Instantiate(_providerList[UnityEngine.Random.Range(0, _providerList.Count)], selected.StartPos, Quaternion.identity);
        obj.transform.DOMove(selected.EndPos, _deliverySpeed);

        var item = obj.GetComponent<BaseDeliveryItem>();
        _itemList.Add(item);

        Action handler = () => selected.IsDelivery(false);
        _handlerMap[item] = handler;
        item.OnClicked += handler;
    }

    public void Dispose()
    {
        foreach (var item in _itemList)
        {
            if (_handlerMap.TryGetValue(item, out var handler))
            {
                item.OnClicked -= handler;
            }
        }
        _handlerMap.Clear();
        _itemList.Clear();
    }
}
