using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Economy", menuName = "TrainGame/RunTimeData/Economy")]
    public class Economy : ScriptableObject
    {
        public float FreightHauled { get; private set; }
        public float FreightHauling { get; private set; }
        public float FreightHaulRate { get; private set; }
        public int PassengersCarried { get; private set; }
        public int PassengersCarrying { get; private set; }
        public int PassengerCarryRate { get; private set; }
        
        public event Action OnEconomyChanged;
        
        public void PeriodicUpdate()
        {
            FreightHauling += FreightHaulRate;
            PassengersCarrying += PassengerCarryRate;
            NotifyChange();
        }
        
        public void DeliverFreight(float amount)
        {
            FreightHauling -= amount;
            FreightHauled += amount;
            NotifyChange();
        }
        
        public void DeliverPassengers()
        {
            PassengersCarried += PassengersCarrying;
            NotifyChange();
        }

        public void Initialize()
        {
            FreightHauled = 0;
            PassengersCarried = 0;
            FreightHauling = 0;
            PassengersCarrying = 0;
            FreightHaulRate = 0;
            PassengerCarryRate = 0;
            NotifyChange();
        }

        private void NotifyChange()
        {
            OnEconomyChanged?.Invoke();
        }
    }
}