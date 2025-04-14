using System;
using System.Collections.Generic;
using System.Linq;
using TrainGame.Model.TrainConsist;

namespace TrainGame.Model.Service
{
    public class ServiceQueryService
    {
        private readonly ServiceRepository _repository;
        public ServiceQueryService(ServiceRepository repository)
        {
            _repository = repository;
        }
        
        public bool IsTrainConsistInService(TrainConsistModel trainConsist)
        {
            if (trainConsist == null)
                throw new ArgumentNullException(nameof(trainConsist));
            
            return GetTrainConsistsInService().Contains(trainConsist);
        }

        public HashSet<TrainConsistModel> GetTrainConsistsInService()
        {
            return _repository.List.Select(x => x.TrainConsist).ToHashSet();
        }

        public HashSet<TrainConsistModel> GetTrainConsistsOnStandby()
        {
            return TrainConsistManager.I.AllTrainConsists.Except(GetTrainConsistsInService()).ToHashSet();
        }
    }
}