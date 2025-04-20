using Core.Utility;
using ScriptableObjects;
using TrainGame.Model.Progress;

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
            if (soRoute.isBuiltAtStart)
                SetBuilt();
        }
        
        public BuildState GetRouteBuildState()
        {
            if (!RouteQueryService.IsRouteDepartingStationBuilt(_routeModel))
                return BuildState.NotAvailableForBuilding;

            return GetBuildState();
        }
    }
}