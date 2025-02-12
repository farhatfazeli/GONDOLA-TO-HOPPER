using System.Collections.Generic;
using Persistence;
using Train.Model;

namespace Train.Infrastructure
{
    public abstract class TrainSaveManager
    {
        public static void PopulateSaveData(List<TrainConsistModel> trains, SaveData sd)
        {
            foreach (var train in trains)
            {
                TrainConsistSaveData trainConsistSaveData = new TrainConsistSaveData()
                {
                    uuid = train.uuid,
                    name = train.name,
                    rollingStock = train.RollingStock
                };
                sd.trainConsistSD.Add(trainConsistSaveData);
            }
        }

        public static List<TrainConsistModel> LoadFromSaveData(SaveData sd)
        {
            List<TrainConsistModel> loadedTrains = new List<TrainConsistModel>();

            foreach (var trainConsistSaveData in sd.trainConsistSD)
            {
                TrainConsistModel train = new TrainConsistModel(trainConsistSaveData.uuid, trainConsistSaveData.name, trainConsistSaveData.rollingStock);
                loadedTrains.Add(train);
            }

            return loadedTrains;
        }
    }
}