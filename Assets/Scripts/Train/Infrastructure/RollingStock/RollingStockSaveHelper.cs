using System.Collections.Generic;
using System.Linq;
using Persistence;
using Train.Model.RollingStock;
using Train.Repositories;

namespace Train.Infrastructure.RollingStock
{
    public class RollingStockSaveHelper
    {
        public static void PopulateSaveData(SaveData sd)
        {
            List<RollingStockModel> rollingStockModels = RollingStockRepository.I.GetAllRollingStock();
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
            if(RollingStockRepository.I.GetAllRollingStock().Count == 0)
            {
                throw new System.Exception("RollingStockRepository not populated with rolling stock.");
            }
            
            Dictionary<string, RollingStockSaveData> saveLookup = sd.rollingStockSD.ToDictionary(r => r.uuid);
            
            foreach (RollingStockModel rollingStockModel in RollingStockRepository.I.GetAllRollingStock())
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