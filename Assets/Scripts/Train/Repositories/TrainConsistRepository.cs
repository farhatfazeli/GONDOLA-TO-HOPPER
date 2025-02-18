using System;
using System.Collections.Generic;
using System.Linq;
using Train.Model;

namespace Train.Repositories
{
    public class TrainConsistRepository
    {
        private readonly List<TrainConsistModel> _standbyTrains = new List<TrainConsistModel>();
        private readonly Dictionary<TrainConsistModel, ServiceModel> _trainsInService = new();
        
        public event Action OnTrainListUpdated;
        
        public void AddTrain(TrainConsistModel train)
        {
            _standbyTrains.Add(train);
            OnTrainListUpdated?.Invoke();
        }
        
        public void AddTrains(IEnumerable<TrainConsistModel> trains)
        {
            _standbyTrains.AddRange(trains);
            OnTrainListUpdated?.Invoke();
        }
        
        public void PutTrainInService(TrainConsistModel train, ServiceModel service)
        {
            if (_standbyTrains.Remove(train))
            {
                _trainsInService.Add(train, service);
                OnTrainListUpdated?.Invoke();
            }
            else
            {
                throw new InvalidOperationException($"Train {train.name} is not in standby!");
            }
        }
        
        public void RemoveTrainFromService(TrainConsistModel train)
        {
            if (_trainsInService.Remove(train))
            {
                _standbyTrains.Add(train);
                OnTrainListUpdated?.Invoke();
            }
            else
            {
                throw new InvalidOperationException($"Train {train.name} is not in service!");
            }
        }
        
        public void RemoveTrainFromService(ServiceModel service)
        {
            TrainConsistModel train = _trainsInService.FirstOrDefault(x => x.Value == service).Key;
            RemoveTrainFromService(train);
        }

        public void RemoveTrain(TrainConsistModel train)
        {
            _standbyTrains.Remove(train);
            OnTrainListUpdated?.Invoke();
        }

        public List<TrainConsistModel> GetAllTrains()
        {
            List<TrainConsistModel> allTrains = GetStandbyTrains();
            allTrains.AddRange(GetTrainsInService());
            return allTrains;
        }
        
        public List<TrainConsistModel> GetStandbyTrains()
        {
            return new List<TrainConsistModel>(_standbyTrains);
        }
        
        public List<TrainConsistModel> GetTrainsInService()
        {
            return new List<TrainConsistModel>(_trainsInService.Keys);
        }
        
        public bool IsTrainInService(TrainConsistModel train)
        {
            return _trainsInService.ContainsKey(train);
        }
        
        public List<ServiceModel> GetActiveServices()
        {
            return new List<ServiceModel>(_trainsInService.Values);
        }
        
        public void Clear()
        {
            _standbyTrains.Clear();
            _trainsInService.Clear();
            OnTrainListUpdated?.Invoke();
        }
        
        private static TrainConsistRepository instance;
        public static TrainConsistRepository I => instance ??= new TrainConsistRepository();
        private TrainConsistRepository() { }
    }
}