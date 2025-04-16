using System;
using Core.Utility;
using ScriptableObjects;
using TrainGame.Model.Station;

namespace TrainGame.Model.Route
{
    public class RouteModel : IIdentifiable, IModelObservable
    {
        public string uuid { get; }
        public string name { get; }


        public StationModel departureStation;
        public StationModel arrivalStation;
        public float distance;

        public readonly RouteBuilder routeBuilder;

        public RouteModel(SO_Route route)
        {
            uuid = route.uuid;
            name = route.name;
            departureStation = StationManager.I.QueryService.GetModel(route.departureStation);
            arrivalStation = StationManager.I.QueryService.GetModel(route.arrivalStation);
            distance = route.distance;

            routeBuilder = new RouteBuilder(route);
        }

        public event Action OnModelChanged;
    }
}