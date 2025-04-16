using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TrainGame.Model.Route;
using TrainGame.Model.TrainConsist;

namespace TrainGame.Model.Service
{
    public class ServiceManager
    {
        private readonly ServiceRepository _repository;
        
        public readonly ServiceQueryService QueryService;
        
        public event Action OnServiceListUpdated
        {
            add => _repository.ListUpdated += value;
            remove => _repository.ListUpdated -= value;
        }
        
        /// <summary>
        /// Starts a new service for a given train using the provided route
        /// </summary>
        public ServiceModel CreateService(RouteModel routeModel, TrainConsistModel trainConsist)
        {
            if(routeModel == null || trainConsist == null) 
                throw new ArgumentNullException(nameof(routeModel) +  "." + nameof(trainConsist));
            
            if (QueryService.IsTrainConsistInService(trainConsist))
                throw new InvalidOperationException("The trainConsist is already in service.");

            string serviceName = GetServiceName(routeModel, trainConsist);
            
            ServiceModel serviceModel = new ServiceModel(serviceName, routeModel, trainConsist);
            
            _repository.Add(serviceModel);

            return serviceModel;
        }

        // public bool FinishService(ServiceModel service)
        // {
        //     if (!service.IsComplete)
        //     {
        //         throw new InvalidOperationException("Service is not complete!");
        //     }
        //     
        //     TrainConsistRepository.I.RemoveTrainFromService(service);
        //
        //     OnServiceListUpdated?.Invoke();
        //     return true;
        // }
        //
        public void UpdateServices(float deltaTime)
        {
            foreach (ServiceModel service in _repository.List)
            {
                if (service.IsComplete) continue;
                service.Update(deltaTime);
            }
        }

        private static string GetServiceName(RouteModel routeModel, TrainConsistModel trainConsist)
        {
            return $"{trainConsist.name} on {routeModel.name}";
        }
        
        private static ServiceManager instance;
        public static ServiceManager I => instance ??= new ServiceManager();
        private ServiceManager()
        {
            _repository = new ServiceRepository();
            QueryService = new ServiceQueryService(_repository);
        }
    }
}