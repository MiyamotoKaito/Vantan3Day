using System;
using UnityEngine.UI;
using UnityEngine;
namespace Ingame
{
    public class Battely : MonoBehaviour
    {
        [SerializeField]
        private float _energy = 50;
        [SerializeField]
        private float _maxEnergy = 100;

        [SerializeField]
        private Image _energyBar;

        public event Action Event;

        private void Update()
        {
            if (_energyBar != null)
            {
                _energyBar.fillAmount = _energy / _maxEnergy;
            }
        }

        public float Energy
        {
            get => _energy;
            set => _energy = Mathf.Clamp(value, 0, _maxEnergy);
        }
        public void ConsumeEnergy(float amount)
        {
            Energy -= amount;
        }
        public void RechargeEnergy(float amount)
        {
            Energy += amount;
        }

        public void TriggerEvent()
        {
            if(Energy >= _maxEnergy * 0.2f)
            {
                ConsumeEnergy(_maxEnergy * 0.2f);
            }
            else
            {
                Debug.Log("エネルギーが不足しています！！");
                return;
            }
            Event?.Invoke();
        }
    }
}