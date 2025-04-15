using System.Collections.Generic;
using Core.Utility;
using ScriptableObjects;
using UnityEngine;

namespace TrainGame.Model.RollingStock
{
    public class RollingStockRepository : DictionaryRepository<RollingStockModel, SO_RollingStock>
    {
        public IReadOnlyDictionary<RollingStockModel, SO_RollingStock> Lookup => _lookup;
        
        
    }
}