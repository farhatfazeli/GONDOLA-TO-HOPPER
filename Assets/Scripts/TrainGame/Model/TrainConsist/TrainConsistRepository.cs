using System;
using System.Collections.Generic;
using System.Linq;
using Core.Utility;
using TrainGame.Model.Service;

namespace TrainGame.Model.TrainConsist
{
    public class TrainConsistRepository : ModelRepository<TrainConsistModel>
    {
        public IReadOnlyCollection<TrainConsistModel> List => _list;
    }
}