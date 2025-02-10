using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;

namespace Trains.Model
{
    public class TrainModel
    {
        private readonly Locomotive _locomotive;
        private readonly List<Wagon> _wagons;
        
        private TrainEngine _trainEngine;
        
        public TrainModel(Locomotive locomotive)
        {
            _locomotive = locomotive;
            _wagons = new List<Wagon>();
            _trainEngine = new TrainEngine(locomotive, GetTotalMass());
        }
        
        public void Update(float deltaTime)
        {
            _trainEngine.Update(deltaTime);
        }
        
        public void StartEngine()
        {
            _trainEngine.Start();
        }
        
        public void AddWagon(Wagon wagon)
        {
            _wagons.Add(wagon);
        }

        public (float, float, float, float) TransmitTrainState()
        {
            return (_trainEngine.Speed, _trainEngine.Acceleration, _trainEngine.TractionForce, 80000);
        }

        public float TransmitCurrentSpeed()
        {
            return _trainEngine.Speed;
        }

        public float TransmitCurrentPosition()
        {
            return _trainEngine.Position;
        }
        
        private float GetTotalMass()
        {
            //return _locomotive.mass + _wagons.Sum(wagon => wagon.mass);
            return 80000;
        }
    }
}