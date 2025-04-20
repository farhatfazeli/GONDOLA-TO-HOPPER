using System.ComponentModel;
using Core.Utility;
using ScriptableObjects;
using TrainGame.Model.Progress;
using UnityEngine;

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
            
            Debug.Log("In station builder constructor: buildprogress = " + BuildProgress);
            Debug.Log("Is build finished in constructor?: " + IsBuilt);
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