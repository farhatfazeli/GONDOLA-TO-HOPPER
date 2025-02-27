using System.Collections.Generic;
using System.Linq;
using Persistence;
using TrainGame.Model.Station;
using TrainGame.Repositories;

namespace TrainGame.Infrastructure.Station
{
    public abstract class StationSaveHelper
    {
        public static void PopulateSaveData(SaveData sd)
        {
            List<StationModel> stations = StationRepository.I.GetAllStations();
            foreach (var station in stations)
            {
                var stationSaveData = new StationSaveData
                {
                    uuid = station.uuid,
                    name = station.name,
                    isBuilt = station.stationBuilder.IsBuilt,
                    passengerLoadRate = station.passengerLoadRate,
                    freightLoadRate = station.freightLoadRate
                };
                sd.stationSD.Add(stationSaveData);
            }
        }

        public static void LoadFromSaveData(SaveData sd)
        {
            if (StationRepository.I.GetAllStations().Count == 0)
            {
                throw new System.Exception("StationRepository not populated with stations.");
            }
            
            Dictionary<string, StationSaveData> saveLookup = sd.stationSD.ToDictionary(s => s.uuid);

            foreach (StationModel station in StationRepository.I.GetAllStations())
            {
                if (saveLookup.TryGetValue(station.uuid, out StationSaveData saved))
                {
                    station.IsBuilt = saved.isBuilt;
                    station.passengerLoadRate = saved.passengerLoadRate;
                    station.freightLoadRate = saved.freightLoadRate;
                }
            }
        }
    }
}