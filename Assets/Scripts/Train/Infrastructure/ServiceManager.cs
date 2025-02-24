using System;
using System.Collections.Generic;
using ScriptableObjects;
using Train.Model;
using Train.Repositories;

namespace Train.Infrastructure
{
    public class ServiceManager
    {
        public event Action OnServiceListUpdated;

        /// <summary>
        /// Starts a new service for a given train using the provided route
        /// </summary>
        public bool CreateService(Route route, TrainConsistModel trainConsist)
        {
            if (TrainConsistRepository.I.IsTrainInService(trainConsist))
            {
                Console.WriteLine($"Train {trainConsist.name} is already in service!");
                return false;
            }

            ServiceModel serviceModel = new ServiceModel(route, trainConsist);
            
            TrainConsistRepository.I.PutTrainInService(trainConsist, serviceModel);

            OnServiceListUpdated?.Invoke();
            return true;
        }

        public bool FinishService(ServiceModel service)
        {
            if (!service.IsComplete)
            {
                throw new InvalidOperationException("Service is not complete!");
            }
            
            TrainConsistRepository.I.RemoveTrainFromService(service);

            OnServiceListUpdated?.Invoke();
            return true;
        }
        
        public void UpdateServices(float deltaTime)
        {
            foreach (ServiceModel service in TrainConsistRepository.I.GetActiveServices())
            {
                if (service.IsComplete) return;
                service.Update(deltaTime);
            }
        }
        
        private static ServiceManager instance;
        public static ServiceManager I => instance ??= new ServiceManager();
        private ServiceManager()
        {
        }
    }
}