using System.Collections.Generic;
using System.Linq;
using Persistence;
using TrainGame.Model.RollingStock;
using TrainGame.Repositories;
using UnityEngine;

namespace TrainGame.Infrastructure.RollingStock
{
    public abstract class RollingStockSaveHelper
    {
        public static void PopulateSaveData(SaveData sd)
        {
            IEnumerable<RollingStockModel> rollingStockModels = RollingStockDictionaryRepository.I.GetModels();
            foreach (var rollingStockModel in rollingStockModels)
            {
                var rollingStockSaveData = new RollingStockSaveData
                {
                    uuid = rollingStockModel.uuid,
                    availableAmount = rollingStockModel.AvailableAmount,
                    fleetAmount = rollingStockModel.FleetAmount
                };
                sd.rollingStockSD.Add(rollingStockSaveData);
            }
        }
        
        public static void LoadFromSaveData(SaveData sd)
        {
            if(RollingStockDictionaryRepository.I.GetModels().ToList().Count == 0)
            {
                throw new System.Exception("RollingStockRepository not populated with rolling stock.");
            }
            
            Dictionary<string, RollingStockSaveData> saveLookup = sd.rollingStockSD.ToDictionary(r => r.uuid);
            
            foreach (RollingStockModel rollingStockModel in RollingStockDictionaryRepository.I.GetModels())
            {
                if (saveLookup.TryGetValue(rollingStockModel.uuid, out RollingStockSaveData saved))
                {
                    rollingStockModel.AvailableAmount = saved.availableAmount;
                    rollingStockModel.FleetAmount = saved.fleetAmount;
                }
            }
        }
    }
}