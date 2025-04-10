using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScriptableObjects;
using TrainGame.Model.RollingStock;
using TrainGame.Model.Station;
using TrainGame.Repositories;
using Utility;

namespace TrainGame.Infrastructure.Station
{
    public abstract class StationLoader
    {
        public static async Task LoadAllStationModelsAsync(string label)
        {
            List<SO_Station> stationAssets = await AddressableLoader<SO_Station>.LoadAllAsync(label);

            var stationModels = new List<StationModel>();

            foreach (var station in stationAssets)
            {
                stationModels.Add(new StationModel(station));
            }

            StationDictionaryRepository.I.Initialize(stationModels, stationAssets);
        }
    }
}
