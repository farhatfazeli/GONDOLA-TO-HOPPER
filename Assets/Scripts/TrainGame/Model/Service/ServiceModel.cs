using System;
using Core.Utility;
using TrainGame.Model.Route;
using TrainGame.Model.Station;
using TrainGame.Model.TrainConsist;
using UnityEngine;

namespace TrainGame.Model.Service
{
    public enum ServiceStatus
    {
        WaitingInDepot,
        Loading,
        Travelling,
        Unloading,
        Finished
    }

    public class ServiceModel : IIdentifiable, IModelObservable
    {
        public string uuid { get; }
        public string name { get; }
        public event Action OnModelChanged;
        
        public ServiceStatus ServiceStatus
        {
            get => _serviceStatus;
            private set
            {
                _serviceStatus = value; 
                OnModelChanged?.Invoke();
            }
        }

        public readonly ServiceInfo serviceInfo;
        private ServiceStatus _serviceStatus;
        
        public readonly StationMasterModel departureStationMasterModel;
        public readonly StationMasterModel arrivalStationMasterModel;
        public readonly TrainDriver trainDriver;

        // public DateTime departureTime;
        // public DateTime arrivalTime;
        

        public ServiceModel(string name, RouteModel routeModel, TrainConsistModel trainConsist) : this(Guid.NewGuid().ToString(), name, routeModel, trainConsist)
        {
        }

        public ServiceModel(string uuid, string name, RouteModel routeModel, TrainConsistModel trainConsist)
        {
            this.uuid = uuid;
            this.name = name;
            
            serviceInfo = new ServiceInfo(routeModel, trainConsist);
            
            ServiceStatus = ServiceStatus.WaitingInDepot;
            
            departureStationMasterModel = new StationMasterModel(serviceInfo, StationMasterType.DepartingStationMaster);
            arrivalStationMasterModel = new StationMasterModel(serviceInfo, StationMasterType.ArrivingStationMaster);
            trainDriver = new TrainDriver(trainConsist.trainEngine, routeModel);
            
            StartLoading();
        }

        /// <summary>
        /// Begins the loading phase at the departure station.
        /// </summary>
        private void StartLoading()
        {
            Debug.Log("Starting loading...");
            if (ServiceStatus != ServiceStatus.WaitingInDepot)
                throw new InvalidOperationException();
            
            ServiceStatus = ServiceStatus.Loading;
            departureStationMasterModel.StartProcess();
            
            departureStationMasterModel.OnProcessComplete += OnLoadComplete;
        }

        private void OnLoadComplete()
        {
            Debug.Log("OnLoadComplete");
            StartJourney();
            departureStationMasterModel.OnProcessComplete -= OnLoadComplete;
        }

        /// <summary>
        /// Transitions to the travel phase once loading is complete.
        /// </summary>
        private void StartJourney()
        {
            Debug.Log("Starting journey...");
            if (ServiceStatus != ServiceStatus.Loading)
                throw new InvalidOperationException();
            
            ServiceStatus = ServiceStatus.Travelling;
            trainDriver.StartDriving();

            trainDriver.OnJourneyComplete += OnJourneyComplete;
        }

        private void OnJourneyComplete()
        {
            Debug.Log("OnJourneyComplete");
            StartUnloading();
            trainDriver.OnJourneyComplete -= OnJourneyComplete;
        }

        /// <summary>
        /// Begins the unloading phase at the arrival station once travel is complete.
        /// </summary>
        private void StartUnloading()
        {
            Debug.Log("Starting unloading...");
            if (ServiceStatus != ServiceStatus.Travelling) 
                throw new InvalidOperationException();
            
            ServiceStatus = ServiceStatus.Unloading;
            arrivalStationMasterModel.StartProcess();
            
            arrivalStationMasterModel.OnProcessComplete += OnUnloadComplete;
        }

        private void OnUnloadComplete()
        {
            Debug.Log("OnUnloadComplete");
            CompleteService();
            arrivalStationMasterModel.OnProcessComplete -= OnUnloadComplete;
        }

        private void CompleteService()
        {
            Debug.Log("CompleteService");
            if (ServiceStatus != ServiceStatus.Unloading)
                throw new InvalidOperationException();
            
            ServiceStatus = ServiceStatus.Finished;
        }

        /// <summary>
        /// Call this method every frame to update the service.
        /// </summary>
        public void Update(float deltaTime)
        {
            switch (ServiceStatus)
            {
                case ServiceStatus.Loading:
                    departureStationMasterModel.Update(deltaTime);
                    break;
                case ServiceStatus.Travelling:
                    trainDriver.Update(deltaTime);
                    break;
                case ServiceStatus.Unloading:
                    arrivalStationMasterModel.Update(deltaTime);
                    break;
                case ServiceStatus.WaitingInDepot:
                case ServiceStatus.Finished:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
