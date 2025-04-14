using System;
using Core.Utility;
using ScriptableObjects;

namespace TrainGame.Model.RollingStock
{
    public abstract class RollingStockModel : IIdentifiable, IModelObservable
    {
        public string uuid { get; protected set; }
        public string name { get; protected set;}
        
        public event Action OnModelChanged;

        public abstract RollingStockType Type { get; }

        public int mass;

        private int _availableAmount;
        public int AvailableAmount
        {
            get => _availableAmount;
            set
            {
                _availableAmount = value;
                OnModelChanged?.Invoke();
            }
        }
        
        private int _fleetAmount;
        public int FleetAmount
        {
            get => _fleetAmount;
            set
            {
                _fleetAmount = value;
                OnModelChanged?.Invoke();
            }
        }
        
        public int purchaseCost;
    }
}