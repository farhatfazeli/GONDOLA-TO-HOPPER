using System;
using System.Collections.Generic;
using System.Linq;
using Core.Utility;
using TrainGame.Model.RollingStock;
using TrainGame.Model.Service;

namespace TrainGame.Model.TrainConsist
{
    public class TrainConsistModel :  IIdentifiable, IModelObservable
    {
        public string uuid { get; private set; }
        public string name { get; private set; }
        
        public event Action OnModelChanged;
        
        private List<RollingStockModel> _rollingStock;
        
        public TrainEngine trainEngine;

        public List<RollingStockModel> RollingStock
        {
            get => _rollingStock;
            set
            {
                _rollingStock = value ?? throw new ArgumentNullException(nameof(value));
                RecalculateConsist();
            }
        }

        public float passengerLoad;
        public float freightLoad;

        public float maxPassengerLoad;
        public float maxFreightLoad;
        
        private float TotalCurrentLoad => passengerLoad + freightLoad; // Always up-to-date

        public float travelDistance => trainEngine.Position;
        
        public TrainConsistModel(string name, List<RollingStockModel> rollingStock) : this(Guid.NewGuid().ToString(),
            name, rollingStock)
        {
        }

        public TrainConsistModel(string uuid, string name, List<RollingStockModel> rollingStock)
        {
            this.uuid = uuid;
            this.name = name;
            RollingStock = rollingStock;
            RecalculateConsist();
        }

        private void RecalculateConsist()
        {
            maxPassengerLoad = _rollingStock.OfType<WagonModel>()
                .Where(w => w.loadType == LoadType.Passengers)
                .Sum(w => w.mass);
            maxFreightLoad = _rollingStock.OfType<WagonModel>()
                .Where(w => w.loadType == LoadType.Freight)
                .Sum(w => w.mass);
            
            if(maxPassengerLoad + maxFreightLoad == 0)
                throw new InvalidOperationException("Train must have at least one wagon");

            trainEngine = CreateTrainEngine();
        }
        
        private TrainEngine CreateTrainEngine()
        {
            float maxSpeed = _rollingStock.OfType<LocomotiveModel>().Any()
                ? _rollingStock.OfType<LocomotiveModel>().Min(loco => loco.maxSpeed)
                : 0; // If no locomotives, speed is 0.

            float tractionCoefficient = _rollingStock.OfType<LocomotiveModel>().Sum(loco => loco.tractionCoefficient);
            float brakingCoefficient = _rollingStock.OfType<LocomotiveModel>().Sum(loco => loco.brakingCoefficient);
            
            return new TrainEngine(maxSpeed, tractionCoefficient, brakingCoefficient, TotalCurrentLoad);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}