using ScriptableObjects;
using TrainGame.Model.Progress;

namespace TrainGame.Model.Builders
{
    public class StationBuilder : BuilderBase
    {
        public StationBuilder(SO_Station station)
            : base(new ConstructionProgress(station.maxBuildPoints),
                station.baseBuildAutoRate,
                station.buildResourceType,
                station.buildResourceCost,
                station.baseBuildManualRate)
        {
            if (station.isBuiltAtStart)
                SetBuilt();
        }
    }
}