using System.Collections.Generic;
using Core.Utility;
using ScriptableObjects;

namespace TrainGame.Model.Station
{
    public class StationRepository : DictionaryRepository<StationModel, SO_Station>
    {
        public IReadOnlyDictionary<StationModel, SO_Station> Lookup => _lookup;
    }
}
