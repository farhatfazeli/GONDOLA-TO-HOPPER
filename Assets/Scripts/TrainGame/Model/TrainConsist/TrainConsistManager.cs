using System;
using System.Collections.Generic;
using System.Linq;
using TrainGame.Model.RollingStock;

namespace TrainGame.Model.TrainConsist
{
    public class TrainConsistManager
    {
        private readonly TrainConsistRepository _repository;

        public IReadOnlyCollection<TrainConsistModel> AllTrainConsists => _repository.List;
        
        public event Action OnTrainConsistListUpdated
        {
            add => _repository.ListUpdated += value;
            remove => _repository.ListUpdated -= value;
        }

        public bool CreateTrainConsist(string trainName, List<RollingStockModel> rollingStockModels)
        {
            if(rollingStockModels == null)
                throw new ArgumentNullException(nameof(rollingStockModels));
            
            if(!rollingStockModels.Any())
                throw new InvalidOperationException("rollingStockModels contains no elements");
            
            TrainConsistModelFactory trainConsistModelFactory = new TrainConsistModelFactory();
            TrainConsistModel trainConsistModel = trainConsistModelFactory.CreateTrainConsist(trainName, rollingStockModels);
        }
        
        private static TrainConsistManager instance;
        public static TrainConsistManager I => instance ??= new TrainConsistManager();
        private TrainConsistManager()
        {
            _repository = new TrainConsistRepository();
        }
    }
}