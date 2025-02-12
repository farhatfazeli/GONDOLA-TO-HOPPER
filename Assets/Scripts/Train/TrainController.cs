using System;
using System.Collections.Generic;
using Persistence;
using ScriptableObjects;
using Train.Infrastructure;
using Train.Model;

namespace Train
{
    public class TrainController : PersistentSingleton<TrainController>, ISaveable
    {
        private readonly TrainServiceManager _trainServiceManager = new TrainServiceManager();
        private readonly TrainConsistRepository _trainRepository = new TrainConsistRepository();
        private TrainSaveManager _trainSaveManager;

        public event Action OnTrainListUpdated
        {
            add => _trainRepository.OnTrainListUpdated += value;
            remove => _trainRepository.OnTrainListUpdated -= value;
        }
        
        public bool DispatchTrain(TrainConsistModel trainConsist, Route route, LoadType loadType, int capacity)
        {
            if(_trainServiceManager.StartService(route, trainConsist, loadType, capacity))
            {
                _trainRepository.DispatchTrain(trainConsist);
                return true;
            }
            return false;
        }
        
        public bool RecallTrain(TrainConsistModel trainConsist)
        {
            if (_trainServiceManager.FinishService(trainConsist))
            {
                _trainRepository.RecallTrain(trainConsist);
                return true;
            }
            return false;
        }
        
        public void AddTrainConsist(TrainConsistModel trainConsist) => _trainRepository.AddTrain(trainConsist);

        public void RemoveTrainConsist(TrainConsistModel trainConsist) => _trainRepository.RemoveTrain(trainConsist);
        
        public List<TrainConsistModel> GetAllTrainConsists() => _trainRepository.GetAllTrains();
        public List<TrainConsistModel> GetAllTrainConsists(ServiceStatus serviceStatus) => _trainRepository.GetAllTrains(serviceStatus);
        

        public void PopulateSaveData(SaveData sd) => TrainSaveManager.PopulateSaveData(_trainRepository.GetAllTrains(), sd);

        public void LoadFromSaveData(SaveData sd)
        {
            _trainRepository.Clear();
            _trainRepository.AddTrains(TrainSaveManager.LoadFromSaveData(sd));
        }
        
        public void Reset()
        {
            foreach (var train in _trainRepository.GetAllTrains())
            {
                train.Reset();
            }
            _trainRepository.Clear();
        }
    }
}