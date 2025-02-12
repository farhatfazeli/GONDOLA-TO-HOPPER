using System;
using System.Collections.Generic;
using ScriptableObjects;
using Train.Model;
using UnityEngine;

namespace Train.Infrastructure
{
    public enum ServiceStatus
    {
        WaitingInDepot,
        InActiveService
    }
    public class TrainServiceManager
    {
        private readonly Dictionary<TrainConsistModel, ServiceModel> _activeServices = new();
    
        public event Action OnServiceListUpdated;

        public bool StartService(Route route, TrainConsistModel trainConsist, LoadType loadType, int capacity)
        {
            if (IsTrainInService(trainConsist))
            {
                Console.WriteLine($"Train {trainConsist.name} is already in service!");
                return false;
            }

            ServiceModel serviceModel = new ServiceModel(route, trainConsist, loadType, capacity);
            _activeServices.Add(trainConsist, serviceModel);

            OnServiceListUpdated?.Invoke();
            return true;
        }

        public bool FinishService(TrainConsistModel train)
        {
            if (!IsTrainInService(train))
            {
                Console.WriteLine($"Train {train.name} is not in service!");
                return false;
            }

            _activeServices.Remove(train);

            OnServiceListUpdated?.Invoke();
            return true;
        }

        public List<ServiceModel> GetActiveServices()
        {
            return new List<ServiceModel>(_activeServices.Values);
        }

        public bool IsTrainInService(TrainConsistModel train)
        {
            return _activeServices.ContainsKey(train);
        }
    }
}