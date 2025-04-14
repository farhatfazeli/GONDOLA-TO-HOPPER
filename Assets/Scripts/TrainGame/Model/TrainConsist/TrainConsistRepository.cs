using System;
using System.Collections.Generic;
using System.Linq;
using TrainGame.Model.Service;

namespace TrainGame.Model.TrainConsist
{
    public class TrainConsistRepository
    {
        private readonly List<TrainConsistModel> _standbyTrainsConsists = new List<TrainConsistModel>();
        private readonly Dictionary<TrainConsistModel, ServiceModel> _trainsInService = new();
        
        public event Action OnTrainListUpdated;
        
        public void AddTrain(TrainConsistModel train)
        {
            _standbyTrainsConsists.Add(train);
            OnTrainListUpdated?.Invoke();
        }
        
        public void AddTrains(IEnumerable<TrainConsistModel> trains)
        {
            _standbyTrainsConsists.AddRange(trains);
            OnTrainListUpdated?.Invoke();
        }
        
        public void PutTrainInService(TrainConsistModel train, ServiceModel service)
        {
            if (_standbyTrainsConsists.Remove(train))
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
                _standbyTrainsConsists.Add(train);
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
            _standbyTrainsConsists.Remove(train);
            OnTrainListUpdated?.Invoke();
        }

        public List<TrainConsistModel> GetAllTrainConsists()
        {
            List<TrainConsistModel> allTrainConsists = GetStandbyTrainConsists();
            allTrainConsists.AddRange(GetTrainConsistsInService());
            return allTrainConsists;
        }
        
        public List<TrainConsistModel> GetStandbyTrainConsists()
        {
            return new List<TrainConsistModel>(_standbyTrainsConsists);
        }
        
        public List<TrainConsistModel> GetTrainConsistsInService()
        {
            return new List<TrainConsistModel>(_trainsInService.Keys);
        }
        
        public bool IsTrainConsistInService(TrainConsistModel trainConsistModel)
        {
            return _trainsInService.ContainsKey(trainConsistModel);
        }
        
        public List<ServiceModel> GetActiveServices()
        {
            return new List<ServiceModel>(_trainsInService.Values);
        }
        
        public void Clear()
        {
            _standbyTrainsConsists.Clear();
            _trainsInService.Clear();
            OnTrainListUpdated?.Invoke();
        }
        
        private static TrainConsistRepository instance;
        public static TrainConsistRepository I => instance ??= new TrainConsistRepository();
        private TrainConsistRepository() { }
    }
}