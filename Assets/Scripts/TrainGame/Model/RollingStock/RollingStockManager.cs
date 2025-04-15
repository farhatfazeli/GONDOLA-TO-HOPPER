using System;
using System.Collections.Generic;
using ScriptableObjects;

namespace TrainGame.Model.RollingStock
{
    public class RollingStockManager
    {
        private readonly RollingStockRepository _repository;
        
        public readonly RollingStockQueryService QueryService;
        
        public event Action OnRollingStockDictionaryUpdated
        {
            add => _repository.DictionaryUpdated += value;
            remove => _repository.DictionaryUpdated -= value;
        }

        public void InitializeDictionary(List<RollingStockModel> models, List<SO_RollingStock> sos)
        {
            _repository.Initialize(models, sos);
        }
        
        private static RollingStockManager instance;
        public static RollingStockManager I => instance ??= new RollingStockManager();
        private RollingStockManager()
        {
            _repository = new RollingStockRepository();
            QueryService  = new RollingStockQueryService(_repository);
        }
    }
}