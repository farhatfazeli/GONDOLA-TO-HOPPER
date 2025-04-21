using System;
using Core.Utility;
using ScriptableObjects;
using TrainGame.Model.Station;
using UnityEngine;

namespace TrainGame.Model.Route
{
    public class RouteModel : IIdentifiable, IModelObservable
    {
        public string uuid { get; }
        public string name { get; }


        public StationModel departureStation { get;  }
        public StationModel arrivalStation { get; }
        public float distance { get; }

        public readonly RouteBuilder routeBuilder;

        public RouteModel(SO_Route soRoute)
        {
            uuid = soRoute.uuid;
            name = soRoute.name;
            
            if(soRoute.departureStation == null ||  soRoute.arrivalStation == null)
                throw  new NullReferenceException();
            if(soRoute.departureStation == soRoute.arrivalStation)
                throw new ArgumentException();
            
            departureStation = StationManager.I.QueryService.GetModel(soRoute.departureStation);
            arrivalStation = StationManager.I.QueryService.GetModel(soRoute.arrivalStation);
            distance = soRoute.distance;
            
            routeBuilder = new RouteBuilder(this, soRoute);
        }

        public event Action OnModelChanged;
    }
}