using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Economy", menuName = "TrainGame/RunTimeData/Economy")]
    public class Economy : ScriptableObject
    {
        public float FreightHauled { get; private set; }
        public float PassengersCarried { get; private set; }
        
        public Action onEconomyChanged;
        public void AddFreight(float amount)
        {
            FreightHauled += amount;
            NotifyChange();
        }

        public void Initialize()
        {
            FreightHauled = 0;
            PassengersCarried = 0;
            NotifyChange();
        }

        private void NotifyChange()
        {
            onEconomyChanged?.Invoke();
        }
    }
}