using System.Collections.Generic;
using System.Linq;
using Core.Persistence;
using TrainGame.Model.Station;

namespace TrainGame.Infrastructure.SaveHelpers
{
    public abstract class StationSaveHelper
    {
        public static void PopulateSaveData(SaveData sd)
        {
            IEnumerable<StationModel> stationModels = StationManager.I.QueryService.GetModels();
            foreach (var stationModel in stationModels)
            {
                var stationSaveData = new StationSaveData
                {
                    uuid = stationModel.uuid,
                    isBuilt = stationModel.stationBuilder.IsBuilt,
                    buildProgress = stationModel.stationBuilder.BuildProgress,
                    passengerLoadRate = stationModel.passengerLoadRate,
                    freightLoadRate = stationModel.freightLoadRate
                };
                sd.stationSD.Add(stationSaveData);
            }
        }

        public static void LoadFromSaveData(SaveData sd)
        {
            if (StationManager.I.QueryService.GetModels().Count == 0)
            {
                throw new System.Exception("StationRepository not populated with stations.");
            }
            
            Dictionary<string, StationSaveData> saveLookup = sd.stationSD.ToDictionary(s => s.uuid);

            foreach (StationModel station in StationManager.I.QueryService.GetModels())
            {
                if (saveLookup.TryGetValue(station.uuid, out StationSaveData saved))
                {
                    if(saved.isBuilt)
                    {
                        station.stationBuilder.SetBuilt();
                    }
                    else
                    {
                        station.stationBuilder.SetBuildProgress(saved.buildProgress);
                    }
                    station.passengerLoadRate = saved.passengerLoadRate;
                    station.freightLoadRate = saved.freightLoadRate;
                }
            }
        }
    }
}