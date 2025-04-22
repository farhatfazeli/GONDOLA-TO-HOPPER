using System.Collections.Generic;
using System.Linq;
using Core.Utility;
using ScriptableObjects;
using TrainGame.Model.Route;
using UnityEngine;

namespace TrainGame.Model.Station
{
    public class StationQueryService
    {
        private readonly StationRepository _repository;
        public StationQueryService(StationRepository repository)
        {
            _repository = repository;
        }
        
        public SO_Station GetSo(StationModel model)
        {
            return _repository.GetSo(model);
        }

        public HashSet<StationModel> GetBuiltStations()
        {
            return GetStationsByBuildState(BuildState.Built);
        }
        
        public StationModel GetStationModelByName(string name)
        {
            return _repository.GetModels().FirstOrDefault(x => x.name == name);
        }

        public StationModel GetModel(SO_Station so)
        {
            return _repository.GetModel(so);
        }
        
        public HashSet<StationModel> GetModels()
        {
            return _repository.GetModels().ToHashSet();
        }

        public HashSet<StationModel> GetStationsByBuildState(BuildState buildState)
        {
            return GetModels().Where(x => x.stationBuilder.GetStationBuildState() == buildState).ToHashSet();
        }
        
        public HashSet<StationModel> GetStationsNotInBuildState(BuildState buildState)
        {
            return GetModels().Except(GetStationsByBuildState(buildState)).ToHashSet();
        } 
        
        public static bool IsStationConnected(StationModel stationModel)
        {
            return RouteManager.I.QueryService.GetRoutesToStation(stationModel).Any(routeModel => routeModel.routeBuilder.IsBuilt);
        }

        public HashSet<StationModel> GetConnectedStations(StationModel stationModel, BuildState buildState)
        {
            return RouteManager.I.QueryService.
                GetRoutesFromStation(stationModel, BuildState.Built).
                Select(x => x.arrivalStation).
                Where(x => x.stationBuilder.GetStationBuildState() == buildState).
                ToHashSet();
        }
    }
}