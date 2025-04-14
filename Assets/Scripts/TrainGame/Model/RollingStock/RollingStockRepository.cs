using Core.Utility;
using ScriptableObjects;
using UnityEngine;

namespace TrainGame.Model.RollingStock
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