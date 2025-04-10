using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TrainGame.Model.RollingStock;
using TrainGame.Model.Station;
using UnityEngine;

namespace TrainGame.Repositories
{
    public class StationDictionaryRepository : DictionaryRepository<StationModel, SO_Station>
    {
        private static StationDictionaryRepository instance;
        public static StationDictionaryRepository I => instance ??= new StationDictionaryRepository();
        private StationDictionaryRepository()
        {
        }
    }
}
