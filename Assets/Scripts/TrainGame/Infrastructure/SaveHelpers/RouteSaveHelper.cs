using System.Collections.Generic;
using System.Linq;
using Core.Persistence;
using TrainGame.Model.Route;

namespace TrainGame.Infrastructure.SaveHelpers
{
    public abstract class RouteSaveHelper
    {
        public static void PopulateSaveData(SaveData sd)
        {
            IEnumerable<RouteModel> routeModels = RouteRepository.I.GetModels();
            foreach (var routeModel in routeModels)
            {
                var routeSaveData = new RouteSaveData
                {
                    uuid = routeModel.uuid,
                    isBuilt = routeModel.routeBuilder.IsBuilt,
                    buildProgress = routeModel.routeBuilder.BuildProgress
                };
                sd.routeSD.Add(routeSaveData);
            }
        }
        
        public static void LoadFromSaveData(SaveData sd)
        {
            if (RouteRepository.I.GetModels().ToList().Count == 0)
            {
                throw new System.Exception("RouteRepository not populated with routes.");
            }
            
            Dictionary<string, RouteSaveData> saveLookup = sd.routeSD.ToDictionary(s => s.uuid);

            foreach (RouteModel route in RouteRepository.I.GetModels())
            {
                if (saveLookup.TryGetValue(route.uuid, out RouteSaveData saved))
                {
                    if(saved.isBuilt)
                    {
                        route.routeBuilder.SetBuilt();
                    }
                    else
                    {
                        route.routeBuilder.SetBuildProgress(saved.buildProgress);
                    }
                }
            }
        }
    }
}