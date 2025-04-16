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
        public readonly StationModel DepartureStationModel;
        public readonly StationModel ArrivalStationModel;
        
        public readonly StationMasterModel DepartureStationMasterModel;
        public readonly StationMasterModel ArrivalStationMasterModel;
        public readonly TrainDriver trainDriver;

        // public DateTime departureTime;
        // public DateTime arrivalTime;
        
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
            DepartureStationModel = RouteModel.departureStation;
            ArrivalStationModel = RouteModel.arrivalStation;
            
            DepartureStationMasterModel = new StationMasterModel(DepartureStationModel, StationMasterType.DepartingStationMaster, trainConsist);
            ArrivalStationMasterModel = new StationMasterModel(ArrivalStationModel, StationMasterType.ArrivingStationMaster, trainConsist);
            trainDriver = new TrainDriver(trainConsist.engine, routeModel);
            
            StartLoading();
        }

        /// <summary>
        /// Begins the loading phase at the departure station.
        /// </summary>
        public void StartLoading()
        {
            if (ServiceStatus != ServiceStatus.WaitingInDepot) return;
            ServiceStatus = ServiceStatus.Loading;
            DepartureStationMasterModel.StartProcess();
        }

        /// <summary>
        /// Transitions to the travel phase once loading is complete.
        /// </summary>
        private void StartTravelling()
        {
            if (ServiceStatus != ServiceStatus.Loading) return;
            if (!DepartureStationMasterModel.IsProcessFinished) return;
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
            ArrivalStationMasterModel.StartProcess();
        }

        public void CompleteService()
        {
            if (ServiceStatus != ServiceStatus.Unloading) return;
            if (!ArrivalStationMasterModel.IsProcessFinished) return;
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
                    DepartureStationMasterModel.Update(deltaTime);
                    break;
                case ServiceStatus.Travelling:
                    trainDriver.Update(deltaTime);
                    break;
                case ServiceStatus.Unloading:
                    ArrivalStationMasterModel.Update(deltaTime);
                    break;
                case ServiceStatus.WaitingInDepot:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
