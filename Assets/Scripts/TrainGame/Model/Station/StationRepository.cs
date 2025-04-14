using Core.Utility;
using ScriptableObjects;

namespace TrainGame.Model.Station
{
    public class StationRepository : DictionaryRepository<StationModel, SO_Station>
    {
        private static StationRepository instance;
        public static StationRepository I => instance ??= new StationRepository();
        private StationRepository()
        {
        }
    }
}
