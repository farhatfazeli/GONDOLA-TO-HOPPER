using ScriptableObjects;
using TrainGame.Model;
using TrainGame.Model.Route;

namespace TrainGame.Repositories
{
    public class RouteRepository : Repository<RouteModel, SO_Route>
    {
        private static RouteRepository instance;
        public static RouteRepository I => instance ??= new RouteRepository();
        private RouteRepository()
        {
        }
    }
}