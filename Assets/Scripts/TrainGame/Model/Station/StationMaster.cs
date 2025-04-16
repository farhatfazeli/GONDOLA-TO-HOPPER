using System;
using ScriptableObjects;
using TrainGame.Model.Progress;
using TrainGame.Model.TrainConsist;

namespace TrainGame.Model.Station
{
    public enum StationMasterType
    {
        DepartingStationMaster,
        ArrivingStationMaster
    }

    public class StationMasterModel
    {
        public readonly ProgressTracker _passengerProgressTracker;
        public readonly ProgressTracker _freightProgressTracker;
        public bool IsProcessFinished => _passengerProgressTracker.IsFinished && _freightProgressTracker.IsFinished;
        public event Action OnProcessComplete;
        
        private readonly float _baseLoadManualRate;
        private TrainConsistModel _train;
        
        public StationMasterModel(StationModel stationModel, StationMasterType stationMasterType, TrainConsistModel train)
        {
            _baseLoadManualRate = stationModel.baseLoadManualRate;

            switch (stationMasterType)
            {
                case StationMasterType.DepartingStationMaster:
                    _passengerProgressTracker = new ProgressTracker(new LoadProgress(train.maxPassengerLoad, LoadMode.Loading), stationModel.baseLoadAutoRate);
                    _freightProgressTracker = new ProgressTracker(new LoadProgress(train.maxFreightLoad, LoadMode.Loading), stationModel.baseLoadAutoRate);
                    break;
                case StationMasterType.ArrivingStationMaster:
                    _passengerProgressTracker = new ProgressTracker(new LoadProgress(train.maxPassengerLoad, LoadMode.Unloading), stationModel.baseLoadAutoRate);
                    _freightProgressTracker = new ProgressTracker(new LoadProgress(train.maxFreightLoad, LoadMode.Unloading), stationModel.baseLoadAutoRate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stationMasterType), stationMasterType, null);
            }
            
            stationModel
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
            _passengerProgressTracker.AdvanceBy(_baseLoadManualRate);
            _freightProgressTracker.AdvanceBy(_baseLoadManualRate);
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
