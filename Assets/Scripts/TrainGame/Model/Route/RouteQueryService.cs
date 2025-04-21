using System.Collections.Generic;
using System.Linq;
using Core.Utility;
using ScriptableObjects;
using TrainGame.Model.Station;

namespace TrainGame.Model.Route
{
    public class RouteQueryService
    {
        private readonly RouteRepository _repository;
        public RouteQueryService(RouteRepository repository)
        {
            _repository = repository;
        }
        
        public List<RouteModel> GetBuiltRoutes()
        {
            return _repository.GetModels().Where(x => x.routeBuilder.IsBuilt).ToList();
        }

        public RouteModel GetRouteModelByName(string name)
        {
            return _repository.GetModels().FirstOrDefault(x => x.name == name);
        }
        
        public SO_Route GetSo(RouteModel model)
        {
            return _repository.GetSo(model);
        }

        public RouteModel GetModel(SO_Route so)
        {
            return _repository.GetModel(so);
        }
        
        public HashSet<RouteModel> GetModels()
        {
            return _repository.GetModels().ToHashSet();
        }

        public HashSet<RouteModel> GetRoutesByBuildState(BuildState buildState)
        {
            return GetModels().Where(x=>x.routeBuilder.GetRouteBuildState() == buildState).ToHashSet();
        }

        public HashSet<RouteModel> GetRoutesToStation(StationModel stationModel)
        {
            return _repository.GetModels().Where(x => x.arrivalStation == stationModel).ToHashSet();
        }

        public static bool IsRouteDepartingStationBuilt(RouteModel routeModel)
        {
            return routeModel.departureStation.stationBuilder.IsBuilt;
        }
    }
}