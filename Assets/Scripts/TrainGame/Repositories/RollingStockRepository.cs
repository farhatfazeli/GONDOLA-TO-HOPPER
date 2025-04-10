using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TrainGame.Model.RollingStock;
using UnityEngine;
using Utility;

namespace TrainGame.Repositories
{
    public class RollingStockRepository : DictionaryRepository<RollingStockModel, SO_RollingStock>
    {
        public GameObject GetViewPrefab(RollingStockModel rollingStockModel)
        {
            return GetSo(rollingStockModel).viewGo;
        }
        
        private static RollingStockRepository instance;
        public static RollingStockRepository I => instance ??= new RollingStockRepository();
        private RollingStockRepository()
        {
        }
    }
}