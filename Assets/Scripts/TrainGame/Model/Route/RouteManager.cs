using System;
using System.Collections.Generic;
using ScriptableObjects;
using TrainGame.Model.RollingStock;

namespace TrainGame.Model.Route
{
    public class RouteManager
    {
        private readonly RouteRepository _repository;
        
        public readonly RouteQueryService QueryService;
        
        public event Action OnRouteDictionaryUpdated
        {
            add => _repository.DictionaryUpdated += value;
            remove => _repository.DictionaryUpdated -= value;
        }

        public void InitializeDictionary(List<RouteModel> models, List<SO_Route> sos)
        {
            _repository.Initialize(models, sos);
        }
        
        private static RouteManager instance;
        public static RouteManager I => instance ??= new RouteManager();
        private RouteManager()
        {
            _repository = new RouteRepository();
            QueryService = new RouteQueryService(_repository);
        } 
    }
}