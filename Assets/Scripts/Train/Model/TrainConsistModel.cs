using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Train.Infrastructure;

namespace Train.Model
{
    public class TrainConsistModel
    {
        public readonly int uuid;
        public readonly string name;
        private List<RollingStock> _rollingStock;
        
        public TrainEngine engine;

        public List<RollingStock> RollingStock
        {
            get => _rollingStock;
            set
            {
                _rollingStock = value ?? throw new ArgumentNullException(nameof(value));
                RecalculateConsist();
            }
        }

        public Load PassengerLoad { get; private set; }
        public Load FreightLoad { get; private set; }
        private float TotalCurrentLoad => PassengerLoad.Current + FreightLoad.Current; // Always up-to-date
        
        public TrainConsistModel(string name, List<RollingStock> rollingStock) : this(Guid.NewGuid().GetHashCode(),
            name, rollingStock)
        {
        }

        public TrainConsistModel(int uuid, string name, List<RollingStock> rollingStock)
        {
            this.uuid = uuid;
            this.name = name;
            RollingStock = rollingStock;
            RecalculateConsist();
        }

        private void RecalculateConsist()
        {
            float maxPassengerLoad = _rollingStock.OfType<Wagon>()
                .Where(w => w.loadType == LoadType.Passengers)
                .Sum(w => w.mass);
            float maxFreightLoad = _rollingStock.OfType<Wagon>()
                .Where(w => w.loadType == LoadType.Freight)
                .Sum(w => w.mass);
            
            PassengerLoad = new Load(maxPassengerLoad);
            FreightLoad = new Load(maxFreightLoad);

            engine = CreateTrainEngine();
        }
        
        private TrainEngine CreateTrainEngine()
        {
            float maxSpeed = _rollingStock.OfType<Locomotive>().Any()
                ? _rollingStock.OfType<Locomotive>().Min(loco => loco.maxSpeed)
                : 0; // If no locomotives, speed is 0.

            float tractionCoefficient = _rollingStock.OfType<Locomotive>().Sum(loco => loco.tractionCoefficient);
            float brakingCoefficient = _rollingStock.OfType<Locomotive>().Sum(loco => loco.brakingCoefficient);
            
            return new TrainEngine(maxSpeed, tractionCoefficient, brakingCoefficient, TotalCurrentLoad);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}