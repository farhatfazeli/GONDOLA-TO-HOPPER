using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TrainGame.Model.RollingStock;
using UnityEngine;
using Utility;

namespace TrainGame.Repositories
{
    public class RollingStockDictionaryRepository : DictionaryRepository<RollingStockModel, SO_RollingStock>
    {
        public GameObject GetViewPrefab(RollingStockModel rollingStockModel)
        {
            return GetSo(rollingStockModel).viewGo;
        }
        
        private static RollingStockDictionaryRepository instance;
        public static RollingStockDictionaryRepository I => instance ??= new RollingStockDictionaryRepository();
        private RollingStockDictionaryRepository()
        {
        }
    }
}