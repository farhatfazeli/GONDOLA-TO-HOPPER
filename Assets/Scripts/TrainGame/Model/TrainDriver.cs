using System;
using ScriptableObjects;

namespace TrainGame.Model
{
    public class TrainDriver
    {
        private readonly JourneyModel _journeyModel;
        private readonly ProgressTracker _travelProgress;
        
        public bool IsTravelComplete => _travelProgress.IsFinished;
        
        public event Action OnTravelComplete;
        
        private readonly TrainEngine _engine;

        private bool _isTravelStarted;
        
        public TrainDriver(TrainEngine engine, SO_Route soRoute)
        {
            _engine = engine;
            _journeyModel = new JourneyModel(soRoute.distance, _engine.CalculateBrakingDistance());
            _journeyModel.OnBrakingDistanceReached += StartBraking;
            _travelProgress = new ProgressTracker(_journeyModel, engine.Speed);
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