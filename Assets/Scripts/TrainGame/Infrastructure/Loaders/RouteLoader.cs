

using System.Collections.Generic;
using System.Threading.Tasks;
using ScriptableObjects;
using TrainGame.Model.Route;
using TrainGame.Repositories;
using Utility;

namespace TrainGame.Infrastructure.Route
{
    public abstract class RouteLoader
    {
        public static async Task LoadAllRouteModelsAsync(string label)
        {
            List<SO_Route> routeAssets = await AddressableLoader<SO_Route>.LoadAllAsync(label);

            var routeModels = new List<RouteModel>();

            foreach (var route in routeAssets)
            {
                routeModels.Add(new RouteModel(route));
            }

            RouteRepository.I.Initialize(routeModels, routeAssets);
        }
    }
}