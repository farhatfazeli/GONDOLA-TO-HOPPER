using System.Collections.Generic;
using System.Linq;
using TrainGame.Model.Route;

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

        public TrainConsistModel GetTrainConsistByName(string trainName)
        {
            return _repository.List.FirstOrDefault(x => x.name == trainName);
        }
    }
}