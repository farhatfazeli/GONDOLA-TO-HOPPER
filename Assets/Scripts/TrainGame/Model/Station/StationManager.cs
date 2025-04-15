using System;
using System.Collections.Generic;
using ScriptableObjects;

namespace TrainGame.Model.Station
{
    public class StationManager
    {
        private readonly StationRepository _repository;
        
        public readonly StationQueryService QueryService;
        
        public event Action OnStationDictionaryUpdated
        {
            add => _repository.DictionaryUpdated += value;
            remove => _repository.DictionaryUpdated -= value;
        }

        public void InitializeDictionary(List<StationModel> models, List<SO_Station> sos)
        {
            _repository.Initialize(models, sos);
        }
        
        private static StationManager instance;
        public static StationManager I => instance ??= new StationManager();
        private StationManager()
        {
            _repository = new StationRepository();
            QueryService = new StationQueryService(_repository);
        }
    }
}