using System.ComponentModel;
using Core.Utility;
using ScriptableObjects;
using TrainGame.Model.Progress;

namespace TrainGame.Model.Station
{
    public class StationBuilder : BuilderBase
    {
        private StationModel _stationModel;
        public StationBuilder(SO_Station soStation, StationModel stationModel)
            : base(new ConstructionProgress(soStation.maxBuildPoints),
                soStation.baseBuildAutoRate,
                soStation.buildResourceType,
                soStation.buildResourceCost,
                soStation.baseBuildManualRate)
        {
            _stationModel = stationModel;
            if (soStation.isBuiltAtStart)
                SetBuilt();
        }
        
        public BuildState GetBuildState()
        {
            if (!StationManager.I.QueryService.IsStationConnected(_stationModel))
                return BuildState.NotAvailableForBuilding;

            return BuildProgress switch
            {
                >= 1f => BuildState.Built,
                >  0f => BuildState.UnderConstruction,
                0f    => BuildState.NotBuilt,  // exact match
                _     => throw new InvalidEnumArgumentException("Invalid build progress")
            };
        }
    }
}