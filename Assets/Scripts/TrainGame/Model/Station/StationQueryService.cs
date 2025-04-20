using System.Collections.Generic;
using System.Linq;
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

        public StationModel GetModel(SO_Station so)
        {
            return _repository.GetModel(so);
        }
        
        public HashSet<StationModel> GetModels()
        {
            return _repository.GetModels().ToHashSet();
        }

        public bool IsStationConnected(StationModel stationModel)
        {
            return RouteManager.I.QueryService.GetRoutesToStation(stationModel).Any(routeModel => routeModel.routeBuilder.IsBuilt);
        }
    }
}