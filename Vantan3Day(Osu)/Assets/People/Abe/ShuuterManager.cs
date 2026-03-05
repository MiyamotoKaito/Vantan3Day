using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;


public class ShuuterManager : MonoBehaviour
{
    [SerializeField] private GameObject _shuuterObj;
    [SerializeField] private float _shutter;
    [SerializeField] private float _animTime;
    [SerializeField] private float _waitTime;
    public async Task Shuuter()
    {
        Debug.LogWarning("シャッター");
        await _shuuterObj.transform.DOMoveY(_shutter, _waitTime).SetEase(Ease.OutBounce).AsyncWaitForCompletion();
        await Task.Delay((int)(_animTime * 1000));
        await _shuuterObj.transform.DOMoveY(11.6f, _waitTime).SetEase(Ease.OutCirc).AsyncWaitForCompletion()    ;
    }

}
