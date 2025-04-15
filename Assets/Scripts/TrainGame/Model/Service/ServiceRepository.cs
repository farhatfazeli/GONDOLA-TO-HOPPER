using System.Collections.Generic;
using Core.Utility;
using TrainGame.Model.TrainConsist;

namespace TrainGame.Model.Service
{
    public class ServiceRepository : ModelRepository<ServiceModel>
    {
        public IReadOnlyCollection<ServiceModel> List => _list;
    }
}