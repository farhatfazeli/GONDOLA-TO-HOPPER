using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TrainGame.Model.RollingStock;
using TrainGame.Model.Station;
using UnityEngine;

namespace TrainGame.Repositories
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
