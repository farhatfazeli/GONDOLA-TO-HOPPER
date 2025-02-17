using System;
using ScriptableObjects;
using UnityEngine;

namespace Train.Model
{
    public class TrainDriver
    {
        private readonly Journey _journey;
        private readonly ProgressTracker _travelProgress;
        
        public bool IsTravelComplete => _travelProgress.IsFinished;
        
        public event Action OnTravelComplete;
        
        private readonly TrainEngine _engine;

        private bool _isTravelStarted;
        
        public TrainDriver(TrainEngine engine, Route route)
        {
            _engine = engine;
            _journey = new Journey(route.distance, _engine.CalculateBrakingDistance());
            _journey.OnBrakingDistanceReached += StartBraking;
            _travelProgress = new ProgressTracker(_journey, engine.Speed);
        }
        
        public void StartDriving()
        {
            if (IsTravelComplete) return;
            _isTravelStarted = true;
            _engine.SetAccelerationMode(true);
        }
        
        private void StartBraking()
        {
            _engine.SetAccelerationMode(false);
        }

        public void Update(float deltaTime)
        {
            if (!_isTravelStarted || IsTravelComplete) return;
            _travelProgress.Advance(deltaTime);
            _engine.Update(deltaTime);
            
            if (IsTravelComplete) OnTravelComplete?.Invoke();
        }
    }
}