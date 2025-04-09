using System;
using System.Collections.Generic;
using TrainGame.Infrastructure;
using TrainGame.Model.RollingStock;
using TrainGame.Model.TrainConsist;
using TrainGame.Repositories;

namespace TrainGame.Model.Yard
{
    public class YardMarshallModel
    {
        private TrainConsistModelFactory _trainConsistModelFactory = new TrainConsistModelFactory();
        
        public List<RollingStockModel> TrainConsistSelection => _trainConsistModelFactory.GetTrainConsistSelection();
        
        public event Action<List<RollingStockModel>> OnTrainConsistChanged;

        public void CreateTrainConsist(string trainName)
        {
            if (TrainConsistSelection.Count == 0)
                throw new InvalidOperationException("Train consist is empty");
            if (string.IsNullOrEmpty(trainName))
                throw new ArgumentException("Train name cannot be empty");
            TrainConsistModel trainConsistModel = _trainConsistModelFactory.CreateTrainConsist(trainName, TrainConsistSelection);
            TrainConsistRepository.I.AddTrain(trainConsistModel);
        }
        
        public void AddRollingStock(RollingStockModel rollingStockModel)
        {
            _trainConsistModelFactory.AddRollinStock(rollingStockModel);
            OnTrainConsistChanged?.Invoke(TrainConsistSelection);
        }

        public void RemoveLastRollingStock()
        {
            if (TrainConsistSelection.Count == 0)
                return;
            _trainConsistModelFactory.RemoveRollingStock(TrainConsistSelection.Count - 1);
        }
        
        public void Reset()
        {
            _trainConsistModelFactory = new TrainConsistModelFactory();
            OnTrainConsistChanged?.Invoke(TrainConsistSelection);
        }
    }
}