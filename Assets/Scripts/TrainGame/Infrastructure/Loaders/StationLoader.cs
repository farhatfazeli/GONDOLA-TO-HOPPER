using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utility;
using ScriptableObjects;
using TrainGame.Model.Station;

namespace TrainGame.Infrastructure.Loaders
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

            StationRepository.I.Initialize(stationModels, stationAssets);
        }
    }
}
