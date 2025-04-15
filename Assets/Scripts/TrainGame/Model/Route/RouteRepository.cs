using System.Collections.Generic;
using System.Linq;
using Core.Utility;
using ScriptableObjects;

namespace TrainGame.Model.Route
{
    public class RouteRepository : DictionaryRepository<RouteModel, SO_Route>
    {
        public IReadOnlyDictionary<RouteModel, SO_Route> Lookup => _lookup;
    }
}