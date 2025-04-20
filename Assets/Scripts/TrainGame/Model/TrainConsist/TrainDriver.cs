using System;
using TrainGame.Model.Progress;
using TrainGame.Model.Route;
using UnityEngine;

namespace TrainGame.Model.TrainConsist
{
    public class TrainDriver
    {
        public readonly ProgressTracker _journeyProgressTracker;
        public bool IsJourneyComplete => _journeyProgressTracker.IsFinished;
        
        public event Action OnJourneyComplete;
        
        private bool _isTravelStarted;
        
        private readonly TrainEngine _engine;
        public TrainDriver(TrainEngine engine, RouteModel routeModel)
        {
            _engine = engine;
            DriveProgress driveProgress = new DriveProgress(routeModel.distance, _engine.CalculateBrakingDelta());
            driveProgress.OnBrakingDistanceReached += StartBraking;
            _journeyProgressTracker = new ProgressTracker(driveProgress, 0);
        }
        
        public void StartDriving()
        {
            if (IsJourneyComplete) return;
            _isTravelStarted = true;
            _engine.SetAccelerationMode(true);
            _journeyProgressTracker.StartAuto();
        }
        
        private void StartBraking()
        {
            _engine.SetAccelerationMode(false);
        }

        public void Update(float deltaTime)
        {
            float oldPosition = _engine.Position;
            if (!_isTravelStarted || IsJourneyComplete) return;
            _journeyProgressTracker.Advance(deltaTime);
            _engine.Update(deltaTime);
            
            float distanceDelta = _engine.Position - oldPosition;
            
            _journeyProgressTracker.AdvanceBy(distanceDelta);
            
            // Debug.Log("Journey progresstracker progress: )" + _journeyProgressTracker.Progress);
            // Debug.Log("Train position: " + _engine.Position);
            
            if (IsJourneyComplete) OnJourneyComplete?.Invoke();
        }
    }
}