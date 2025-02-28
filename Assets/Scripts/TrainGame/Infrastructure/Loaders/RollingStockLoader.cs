using System.Collections.Generic;
using System.Threading.Tasks;
using ScriptableObjects;
using TrainGame.Model.RollingStock;
using TrainGame.Repositories;
using Utility;

namespace TrainGame.Infrastructure.RollingStock
{
    public abstract class RollingStockLoader
    {
        public static async Task LoadAllRollingStockModelsAsync(string label)
        {
            List<SO_RollingStock> rollingStockAssets = await AddressableLoader<SO_RollingStock>.LoadAllAsync(label);
            
            var rollingStockModels = new List<RollingStockModel>();
            
            foreach (var rollingStock in rollingStockAssets)
            {
                switch (rollingStock.Type)
                {
                    case RollingStockType.Locomotive:
                        rollingStockModels.Add(new LocomotiveModel(rollingStock as SO_Locomotive));
                        break;
                    case RollingStockType.Wagon:
                        rollingStockModels.Add(new WagonModel(rollingStock as SO_Wagon));
                        break;
                    default:
                        throw new System.ArgumentOutOfRangeException();
                }
            }
            
            RollingStockRepository.I.Initialize(rollingStockModels, rollingStockAssets);
        }
    }
}