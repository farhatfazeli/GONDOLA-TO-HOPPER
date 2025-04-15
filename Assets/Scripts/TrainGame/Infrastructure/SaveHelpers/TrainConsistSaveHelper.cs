using System.Collections.Generic;
using Core.Persistence;
using TrainGame.Model.RollingStock;
using TrainGame.Model.TrainConsist;

namespace TrainGame.Infrastructure.SaveHelpers
{
    public abstract class TrainConsistSaveHelper
    {
        public static void PopulateSaveData(SaveData sd)
        {
            IEnumerable<TrainConsistModel> trainConsists = TrainConsistManager.I.QueryService.GetAllTrainConsists();
            foreach (var trainConsistModel in trainConsists)
            {
                var trainConsistSaveData = new TrainConsistSaveData
                {
                    uuid = trainConsistModel.uuid,
                    name = trainConsistModel.name,
                    rollingStockUuids = GetRollingStockUuids(trainConsistModel)
                };
                sd.trainConsistSD.Add(trainConsistSaveData);
            }
        }
        
        private static List<string> GetRollingStockUuids(TrainConsistModel trainConsistModel)
        {
            var rollingStockUuids = new List<string>();
            foreach (var rollingStock in trainConsistModel.RollingStock)
            {
                rollingStockUuids.Add(rollingStock.uuid);
            }
            return rollingStockUuids;
        }

        public static void LoadFromSaveData(SaveData sd)
        {
            var loadedTrains = new List<TrainConsistModel>();

            foreach (var trainConsistSaveData in sd.trainConsistSD)
            {
                List<RollingStockModel> rollingStock = GetRollingStockModels(trainConsistSaveData.rollingStockUuids);
                TrainConsistModel trainConsist = new TrainConsistModel(trainConsistSaveData.uuid, trainConsistSaveData.name, rollingStock);
                loadedTrains.Add(trainConsist);
            }
            
            
            TrainConsistManager.I.Clear();
            TrainConsistManager.I.LoadTrainConsists(loadedTrains);
        }
        
        private static List<RollingStockModel> GetRollingStockModels(List<string> rollingStockUuids)
        {
            var rollingStockModels = new List<RollingStockModel>();
            foreach (var uuid in rollingStockUuids)
            {
                var rollingStockModel = RollingStockManager.I.QueryService.GetByUuid(uuid);
                rollingStockModels.Add(rollingStockModel);
            }
            return rollingStockModels;
        }
    }
}