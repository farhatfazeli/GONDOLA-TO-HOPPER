using System;
using Core.Utility;
using TrainGame.Model.Route;
using TrainGame.Model.Station;
using TrainGame.Model.TrainConsist;

namespace TrainGame.Model.Service
{
    public enum ServiceStatus
    {
        WaitingInDepot,
        Loading,
        Travelling,
        Unloading
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
                OnServiceStatusChanged?.Invoke(ServiceStatus);
            }
        }

        public readonly RouteModel RouteModel;
        public readonly TrainConsistModel TrainConsist;

        public readonly StationMaster departureStationMaster;
        public readonly StationMaster arrivalStationMaster;
        public readonly TrainDriver trainDriver;

        /// <summary>
        /// Indicates that the service is complete when the train has finished unloading and returned to depot.
        /// </summary>
        public bool IsComplete => ServiceStatus == ServiceStatus.WaitingInDepot;

        public event Action<ServiceStatus> OnServiceStatusChanged;

        private ServiceStatus _serviceStatus;

        public ServiceModel(string name, RouteModel routeModel, TrainConsistModel trainConsist) : this(Guid.NewGuid().ToString(), name, routeModel, trainConsist)
        {
        }

        public ServiceModel(string uuid, string name, RouteModel routeModel, TrainConsistModel trainConsist)
        {
            this.uuid = uuid;
            this.name = name;
            RouteModel = routeModel;
            TrainConsist = trainConsist;
            trainDriver = new TrainDriver(trainConsist.engine, routeModel);
        }

        /// <summary>
        /// Begins the loading phase at the departure station.
        /// </summary>
        public void StartLoading()
        {
            if (ServiceStatus != ServiceStatus.WaitingInDepot) return;
            ServiceStatus = ServiceStatus.Loading;
            departureStationMaster.StartProcess();
        }

        /// <summary>
        /// Transitions to the travel phase once loading is complete.
        /// </summary>
        private void StartTravelling()
        {
            if (ServiceStatus != ServiceStatus.Loading) return;
            if (!departureStationMaster.IsProcessFinished) return;
            ServiceStatus = ServiceStatus.Travelling;
            trainDriver.StartDriving();
        }

        /// <summary>
        /// Begins the unloading phase at the arrival station once travel is complete.
        /// </summary>
        public void StartUnloading()
        {
            if (ServiceStatus != ServiceStatus.Travelling) return;
            if (!trainDriver.IsTravelComplete) return;
            ServiceStatus = ServiceStatus.Unloading;
            arrivalStationMaster.StartProcess();
        }

        public void CompleteService()
        {
            if (ServiceStatus != ServiceStatus.Unloading) return;
            if (!arrivalStationMaster.IsProcessFinished) return;
            ServiceStatus = ServiceStatus.WaitingInDepot;
        }

        /// <summary>
        /// Call this method every frame to update the service.
        /// </summary>
        public void Update(float deltaTime)
        {
            switch (ServiceStatus)
            {
                case ServiceStatus.Loading:
                    departureStationMaster.Update(deltaTime);
                    break;
                case ServiceStatus.Travelling:
                    trainDriver.Update(deltaTime);
                    break;
                case ServiceStatus.Unloading:
                    arrivalStationMaster.Update(deltaTime);
                    break;
                case ServiceStatus.WaitingInDepot:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
