using Core.Utility;
using ScriptableObjects;

namespace TrainGame.Model.Station
{
    public class StationBuilder : BuilderBase
    {
        private readonly StationModel _stationModel;
        public StationBuilder(StationModel stationModel, SO_Station soStation)
            : base(soStation.maxBuildPoints,
                soStation.baseBuildAutoRate,
                soStation.buildResourceType,
                soStation.buildResourceCost,
                soStation.baseBuildManualRate)
        {
            _stationModel = stationModel;
            if (soStation.isBuiltAtStart)
                SetBuilt();
        }
        
        public BuildState GetStationBuildState()
        {
            if (IsBuilt)
                return BuildState.Built;
            
            if (!StationQueryService.IsStationConnected(_stationModel))
                return BuildState.NotAvailableForBuilding;

            return GetBuildState();
        }
    }
}