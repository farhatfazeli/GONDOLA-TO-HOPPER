using Core.Utility;
using ScriptableObjects;
using UnityEngine;

namespace TrainGame.Model.Route
{
    public class RouteBuilder : BuilderBase
    {
        private readonly RouteModel _routeModel;
        public RouteBuilder(RouteModel routeModel, SO_Route soRoute)
            : base(soRoute.maxBuildPoints,
                soRoute.baseBuildAutoRate,
                soRoute.buildResourceType,
                soRoute.buildResourceCost,
                soRoute.baseBuildManualRate)
        {
            _routeModel = routeModel;
            if (soRoute.isBuiltAtStart)
                SetBuilt();
        }
        
        public BuildState GetRouteBuildState()
        {
            if (_routeModel.name == "Tubeke - Zinnik")
            {
                Debug.Log("What the fuck: " + _routeModel.name);
                Debug.Log("Build state: " +  IsBuilt);
                Debug.Log("Build state: " + GetBuildState());
            }
            
            if (IsBuilt)
                return BuildState.Built;
            
            if (!RouteQueryService.IsRouteDepartingStationBuilt(_routeModel))
                return BuildState.NotAvailableForBuilding;
            
            return GetBuildState();
        }
    }
}