using System.Collections.Generic;
using Persistence;
using Train.Model;
using Train.Repositories;

namespace Train.Infrastructure
{
    public abstract class TrainSaveHelper
    {
        public static void PopulateSaveData(SaveData sd)
        {
            List<TrainConsistModel> trains = TrainConsistRepository.I.GetAllTrains();
            foreach (var train in trains)
            {
                var trainConsistSaveData = new TrainConsistSaveData
                {
                    uuid = train.uuid,
                    name = train.name,
                    rollingStock = train.RollingStock
                };
                sd.trainConsistSD.Add(trainConsistSaveData);
            }
        }

        public static void LoadFromSaveData(SaveData sd)
        {
            var loadedTrains = new List<TrainConsistModel>();

            foreach (var trainConsistSaveData in sd.trainConsistSD)
            {
                var train = new TrainConsistModel(trainConsistSaveData.uuid, trainConsistSaveData.name,
                    trainConsistSaveData.rollingStock);
                loadedTrains.Add(train);
            }
            
            TrainConsistRepository.I.Clear();
            TrainConsistRepository.I.AddTrains(loadedTrains);
        }
    }
}