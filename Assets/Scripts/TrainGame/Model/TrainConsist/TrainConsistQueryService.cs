using System.Collections.Generic;
using System.Linq;
using TrainGame.Model.Route;
using TrainGame.Model.Service;

namespace TrainGame.Model.TrainConsist
{
    public class TrainConsistQueryService
    {
        private readonly TrainConsistRepository _repository;
        public TrainConsistQueryService(TrainConsistRepository repository)
        {
            _repository = repository;
        }

        public IReadOnlyCollection<TrainConsistModel> GetAllTrainConsists()
        {
            return _repository.List;
        }

        public HashSet<TrainConsistModel> GetTrainConsistsOnStandby()
        {
            return TrainConsistManager.I.AllTrainConsists.Except(ServiceManager.I.QueryService.GetTrainConsistsInService()).ToHashSet();
        }
        
        public TrainConsistModel GetTrainConsistByName(string trainName)
        {
            return _repository.List.FirstOrDefault(x => x.name == trainName);
        }
    }
}