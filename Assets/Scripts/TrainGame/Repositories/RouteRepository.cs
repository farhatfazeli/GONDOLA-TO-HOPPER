using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TrainGame.Model;
using TrainGame.Model.Route;

namespace TrainGame.Repositories
{
    public class RouteRepository : DictionaryRepository<RouteModel, SO_Route>
    {
        public List<RouteModel> GetBuiltRoutes()
        {
            return GetModels().Where(x => x.routeBuilder.IsBuilt).ToList();
        }

        public RouteModel GetRouteModelByName(string name)
        {
            return GetModels().FirstOrDefault(x => x.name == name);
        }
        
        private static RouteRepository instance;
        public static RouteRepository I => instance ??= new RouteRepository();
        private RouteRepository()
        {
        }
    }
}