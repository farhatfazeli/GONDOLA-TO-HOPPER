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
        private readonly ProgressTracker _passengerProgress;
        private readonly ProgressTracker _freightProgress;

        public bool IsProcessFinished => _passengerProgress.IsFinished && _freightProgress.IsFinished;

        public event Action OnProcessComplete;


        private readonly StationMasterType _type;

        private bool _isProcessStarted;


        public StationMaster(SO_Station soStation, StationMasterType type, TrainConsistModel train)
        {
            SetLoadMode(type, train);
            _passengerProgress = new ProgressTracker(train.PassengerLoad, soStation.basePassengerLoadRate);
            _freightProgress = new ProgressTracker(train.FreightLoad, soStation.baseFreightLoadRate);
        }

        public void StartProcess()
        {
            if(IsProcessFinished) return;
            _isProcessStarted = true;
        }

        public void Update(float deltaTime)
        {
            if (!_isProcessStarted || IsProcessFinished) return;

            ProcessPassengers(deltaTime);
            ProcessFreight(deltaTime);
            
            FinishProcess();
        }

        private void ProcessPassengers(float deltaTime)
        {
            if (_passengerProgress.IsFinished) return;
            _passengerProgress.Advance(deltaTime);
        }
        
        private void ProcessFreight(float deltaTime)
        {
            if (_freightProgress.IsFinished) return;
            _freightProgress.Advance(deltaTime);
        }

        private void FinishProcess()
        {
            if (!_passengerProgress.IsFinished || !_freightProgress.IsFinished)
                return;
            OnProcessComplete?.Invoke();
        }
        
        public void Pause()
        {
            _passengerProgress.Pause();
            _freightProgress.Pause();
        }
        
        public void Resume()
        {
            _passengerProgress.Resume();
            _freightProgress.Resume();
        }

        private void SetLoadMode(StationMasterType stationMasterType, TrainConsistModel train)
        {
            switch (stationMasterType)
            {
                case StationMasterType.DepartingStationMaster:
                    train.PassengerLoad.ChangeLoadMode(LoadMode.Loading);
                    train.FreightLoad.ChangeLoadMode(LoadMode.Loading);
                    break;
                case StationMasterType.ArrivingStationMaster:
                    train.PassengerLoad.ChangeLoadMode(LoadMode.Unloading);
                    train.FreightLoad.ChangeLoadMode(LoadMode.Unloading);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stationMasterType), stationMasterType, null);
            }
        }
    }
}
