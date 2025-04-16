using TrainGame.Model.Route;
using TrainGame.Model.Station;
using TrainGame.Model.TrainConsist;

namespace TrainGame.Model.Service
{
    public class ServiceInfo
    {
        public RouteModel RouteModel { get; }
        public TrainConsistModel TrainConsist  { get; }
        public StationModel DepartureStationModel { get; }
        public StationModel ArrivalStationModel { get; }

        public ServiceInfo(RouteModel routeModel, TrainConsistModel trainConsistModel)
        {
            RouteModel = routeModel;
            TrainConsist = trainConsistModel;
            DepartureStationModel = RouteModel.departureStation;
            ArrivalStationModel = RouteModel.arrivalStation;
        }
    }
}