using System;
using ScriptableObjects;
using TrainGame.Repositories;

namespace TrainGame.Model.RollingStock
{
    public abstract class RollingStockModel : IIdentifiable
    {
        public string uuid { get; protected set; }
        public string name { get; protected set;}
        public abstract RollingStockType Type { get; }

        public int mass;

        private int _availableAmount;
        public int AvailableAmount
        {
            get => _availableAmount;
            set
            {
                _availableAmount = value;
                onModelChanged?.Invoke();
            }
        }
        
        private int _fleetAmount;
        public int FleetAmount
        {
            get => _fleetAmount;
            set
            {
                _fleetAmount = value;
                onModelChanged?.Invoke();
            }
        }
        
        public event Action onModelChanged;

        public int purchaseCost;
    }
}