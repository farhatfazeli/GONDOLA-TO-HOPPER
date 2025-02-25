using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TrainGame.Model.RollingStock;
using UnityEngine;

namespace TrainGame.Repositories
{
    public class RollingStockRepository
    {
        private Dictionary<RollingStockModel, SO_RollingStock> _rollingStockModelSoLookup;
        
        public void Initialize(List<RollingStockModel> rollingStockModels, List<SO_RollingStock> rollingStockSos)
        {
            _rollingStockModelSoLookup = new Dictionary<RollingStockModel, SO_RollingStock>();
            for (int i = 0; i < rollingStockModels.Count; i++)
            {
                _rollingStockModelSoLookup.Add(rollingStockModels[i], rollingStockSos[i]);
            }
        }
        
        public SO_RollingStock GetRollingStockSo(RollingStockModel rollingStockModel)
        {
            return _rollingStockModelSoLookup[rollingStockModel];
        }
        
        public GameObject GetRollingStockViewPrefab(RollingStockModel rollingStockModel)
        {
            return _rollingStockModelSoLookup[rollingStockModel].viewGo;
        }

        public IEnumerable<RollingStockModel> GetRollingStockModels()
        {
            return _rollingStockModelSoLookup.Keys;
        }
        
        public RollingStockModel GetRollingStockByUuid(string uuid)
        {
            return _rollingStockModelSoLookup.Keys.FirstOrDefault(x => x.uuid == uuid);
        }

        private static RollingStockRepository instance;
        public static RollingStockRepository I => instance ??= new RollingStockRepository();
        private RollingStockRepository()
        {
        }
    }
}