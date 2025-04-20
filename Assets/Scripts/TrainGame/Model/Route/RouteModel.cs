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


        public StationModel departureStation;
        public StationModel arrivalStation;
        public float distance;

        public readonly RouteBuilder routeBuilder;

        public RouteModel(SO_Route soRoute)
        {
            uuid = soRoute.uuid;
            name = soRoute.name;
            departureStation = StationManager.I.QueryService.GetModel(soRoute.departureStation);
            arrivalStation = StationManager.I.QueryService.GetModel(soRoute.arrivalStation);
            distance = soRoute.distance;
            
            Debug.Log("RouteModel initializing routeBuilder");
            Debug.Log("soRoute:" + soRoute.name + " soRoute maxBuildPoints: " + soRoute.maxBuildPoints);
            routeBuilder = new RouteBuilder(this, soRoute);
        }

        public event Action OnModelChanged;
    }
}