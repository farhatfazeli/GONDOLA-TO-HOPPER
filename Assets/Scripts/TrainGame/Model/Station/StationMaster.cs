using System;
using ScriptableObjects;

namespace TrainGame.Model.Station
{
    public enum StationMasterType
    {
        DepartingStationMaster,
        ArrivingStationMaster
    }

    public class StationMaster
    {
        private readonly ProgressTracker _passengerProgressTracker;
        private readonly ProgressTracker _freightProgressTracker;
        public bool IsProcessFinished => _passengerProgressTracker.IsFinished && _freightProgressTracker.IsFinished;
        public event Action OnProcessComplete;
        
        private readonly float _baseManualLoadRate;
        private TrainConsistModel _train;
        
        public StationMaster(SO_Station soStation, StationMasterType stationMasterType, TrainConsistModel train)
        {
            _baseManualLoadRate = soStation.baseManualLoadRate;

            switch (stationMasterType)
            {
                case StationMasterType.DepartingStationMaster:
                    _passengerProgressTracker = new ProgressTracker(new LoadProgress(train.maxPassengerLoad, LoadMode.Loading), soStation.baseAutoLoadRate);
                    _freightProgressTracker = new ProgressTracker(new LoadProgress(train.maxFreightLoad, LoadMode.Loading), soStation.baseAutoLoadRate);
                    break;
                case StationMasterType.ArrivingStationMaster:
                    _passengerProgressTracker = new ProgressTracker(new LoadProgress(train.maxPassengerLoad, LoadMode.Unloading), soStation.baseAutoLoadRate);
                    _freightProgressTracker = new ProgressTracker(new LoadProgress(train.maxFreightLoad, LoadMode.Unloading), soStation.baseAutoLoadRate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stationMasterType), stationMasterType, null);
            }
        }
        
        public void StartProcess()
        {
            if(IsProcessFinished) return;
            _passengerProgressTracker.Start();
            _freightProgressTracker.Start();
        }

        public void ManualProcess()
        {
            if(IsProcessFinished) return;
            _passengerProgressTracker.AdvanceBy(_baseManualLoadRate);
            _freightProgressTracker.AdvanceBy(_baseManualLoadRate);
        }
        
        public void Update(float deltaTime)
        {
            if(IsProcessFinished) return;
            _passengerProgressTracker.Advance(deltaTime);
            _freightProgressTracker.Advance(deltaTime);
        }
        
        public void Pause()
        {
            _passengerProgressTracker.Pause();
            _freightProgressTracker.Pause();
        }
        
        public void Resume()
        {
            _passengerProgressTracker.Resume();
            _freightProgressTracker.Resume();
        }
    }
}
