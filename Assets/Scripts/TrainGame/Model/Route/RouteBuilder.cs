using Core.Utility;
using ScriptableObjects;
using TrainGame.Model.Progress;

namespace TrainGame.Model.Route
{
    public class RouteBuilder : BuilderBase
    {
        public RouteBuilder(SO_Route route)
            : base(new ConstructionProgress(route.maxBuildPoints),
                route.baseBuildAutoRate,
                route.buildResourceType,
                route.buildResourceCost,
                route.baseBuildManualRate)
        {
            if (route.isBuiltAtStart)
                SetBuilt();
        }
    }
}