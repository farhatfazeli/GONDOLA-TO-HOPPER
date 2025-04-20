using System;
using ScriptableObjects;
using TrainGame.Model.Progress;
using TrainGame.Model.Service;
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
        public ProgressTracker _passengerProgressTracker;
        public ProgressTracker _freightProgressTracker;
        public bool IsProcessFinished => _passengerProgressTracker.IsFinished && _freightProgressTracker.IsFinished;

        public event Action OnProcessComplete;
        
        private TrainConsistModel _train;
        private float _baseLoadManualRate;
        
        public StationMasterModel(ServiceInfo serviceInfo, StationMasterType stationMasterType)
        {
            switch (stationMasterType)
            {
                case StationMasterType.DepartingStationMaster:
                    Initialize(serviceInfo.TrainConsist, serviceInfo.RouteModel.departureStation, LoadMode.Loading);
                    break;
                case StationMasterType.ArrivingStationMaster:
                    Initialize(serviceInfo.TrainConsist, serviceInfo.RouteModel.arrivalStation, LoadMode.Unloading); 
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stationMasterType), stationMasterType, null);
            }

            _passengerProgressTracker.OnProgressComplete += CheckBothProcessesComplete;
            _freightProgressTracker.OnProgressComplete += CheckBothProcessesComplete;
        }
        
        private void Initialize(TrainConsistModel trainConsistModel, StationModel stationModel, LoadMode loadMode)
        {
            _passengerProgressTracker = new ProgressTracker(new LoadProgress(trainConsistModel.maxPassengerLoad, loadMode), stationModel.baseLoadAutoRate);
            _freightProgressTracker = new ProgressTracker(new LoadProgress(trainConsistModel.maxFreightLoad, loadMode), stationModel.baseLoadAutoRate);
            _baseLoadManualRate = stationModel.baseLoadManualRate;
        }

        private void CheckBothProcessesComplete()
        {
            if(IsProcessFinished)
                OnProcessComplete?.Invoke();
        }
        
        public void StartProcess()
        {
            if(IsProcessFinished) return;
            _passengerProgressTracker.StartAuto();
            _freightProgressTracker.StartAuto();
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
