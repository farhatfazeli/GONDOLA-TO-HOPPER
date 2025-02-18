using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScriptableObjects;
using Train.Model.Station;
using Utility;

namespace Train.Infrastructure.Station
{
    public static class StationLoader
    {
        /// <summary>
        ///     Loads all Station assets using Addressables and converts them to runtime StationModels.
        /// </summary>
        /// <param name="label">The Addressable label for Station assets.</param>
        /// <returns>A list of StationModel objects.</returns>
        public static async Task<List<StationModel>> LoadAllStationModelsAsync(string label)
        {
            List<SO_Station> stationAssets = await AddressableLoader<SO_Station>.LoadAllAsync(label);
            return stationAssets.Select(station => new StationModel(station)).ToList();
        }
    }
}