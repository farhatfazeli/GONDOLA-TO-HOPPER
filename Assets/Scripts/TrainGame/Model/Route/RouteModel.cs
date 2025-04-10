using System;
using ScriptableObjects;
using TrainGame.Model.RollingStock;
using TrainGame.Repositories;

namespace TrainGame.Model.Route
{
    public class RouteModel : IIdentifiable, IModelObservable
    {
        public string uuid { get; }
        public string name { get; }
        
        public float distance;
        
        public readonly RouteBuilder routeBuilder;
        
        public RouteModel(SO_Route route)
        {
            uuid = route.uuid;
            name = route.name;
            distance = route.distance;
            
            routeBuilder = new RouteBuilder(route);
        }

        public event Action OnModelChanged;
    }
}