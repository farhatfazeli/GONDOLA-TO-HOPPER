using TrainGame.Repositories;

namespace TrainGame.Model
{
    public class RouteModel : IIdentifiable
    {
        public string uuid { get; private set; }
        public string name { get; private set; }
        
        public bool isBuilt;
    }
}