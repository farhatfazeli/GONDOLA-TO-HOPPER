using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TrainGame.Controller;
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
        
        public HashSet<LocomotiveModel> GetLocomotiveModels()
        {
            return GetModels().Where(x => x.Type == RollingStockType.Locomotive).Cast<LocomotiveModel>().ToHashSet();
        }

        public HashSet<WagonModel> GetWagonModels()
        {
            return GetModels().Where(x => x.Type == RollingStockType.Wagon).Cast<WagonModel>().ToHashSet();
        }

        public HashSet<WagonModel> GetPassengerWagonModels()
        {
            return GetWagonModels().Where(x=> x.loadType == LoadType.Passengers).ToHashSet();
        }
        
        public HashSet<WagonModel> GetFreightWagonModels()
        {
            return GetWagonModels().Where(x=> x.loadType == LoadType.Freight).ToHashSet();
        }
    }
}