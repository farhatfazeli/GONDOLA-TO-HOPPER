using System;
using System.Collections.Generic;
using Train.Infrastructure;

namespace Train.Model
{
    public class TrainConsistRepository
    {
        private readonly List<TrainConsistModel> _standbyTrains = new List<TrainConsistModel>();
        private readonly List<TrainConsistModel> _dispatchedTrains = new List<TrainConsistModel>();

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
        
        public void DispatchTrain(TrainConsistModel train)
        {
            if (_standbyTrains.Remove(train))
            {
                _dispatchedTrains.Add(train);
                OnTrainListUpdated?.Invoke();
            }
        }
        
        public void RecallTrain(TrainConsistModel train)
        {
            if (_dispatchedTrains.Remove(train))
            {
                _standbyTrains.Add(train);
                OnTrainListUpdated?.Invoke();
            }
        }

        public void RemoveTrain(TrainConsistModel train)
        {
            _standbyTrains.Remove(train);
            OnTrainListUpdated?.Invoke();
        }

        public List<TrainConsistModel> GetAllTrains()
        {
            var allTrains = new List<TrainConsistModel>(_standbyTrains);
            allTrains.AddRange(_dispatchedTrains);
            return allTrains;
        }
        
        public void Clear()
        {
            _standbyTrains.Clear();
            _dispatchedTrains.Clear();
            OnTrainListUpdated?.Invoke();
        }
    }
}