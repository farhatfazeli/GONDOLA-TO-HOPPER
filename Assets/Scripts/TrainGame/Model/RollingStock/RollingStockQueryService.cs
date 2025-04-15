using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

namespace TrainGame.Model.RollingStock
{
    public class RollingStockQueryService
    {
        private readonly RollingStockRepository _repository;
        public RollingStockQueryService(RollingStockRepository repository)
        {
            _repository = repository;
        }

        public SO_RollingStock GetSo(RollingStockModel model)
        {
            return _repository.GetSo(model);
        }

        public RollingStockModel GetModel(SO_RollingStock so)
        {
            return _repository.GetModel(so);
        }
        
        public HashSet<RollingStockModel> GetModels()
        {
            return _repository.GetModels().ToHashSet();
        }

        public RollingStockModel GetByUuid(string uuid)
        {
            return _repository.GetByUuid(uuid);
        }
        
        public GameObject GetViewPrefab(RollingStockModel rollingStockModel)
        {
            return _repository.GetSo(rollingStockModel).viewGo;
        }
    }
}