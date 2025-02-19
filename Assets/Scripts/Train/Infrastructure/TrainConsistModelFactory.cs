using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Train.Model;
using Train.Model.RollingStock;

namespace Train.Infrastructure
{
    public class TrainConsistModelFactory
    {
        
        
        public TrainConsistModel CreateTrainConsist(string trainName, List<RollingStockModel> rollingStockSelection)
        {
            if (rollingStockSelection == null || rollingStockSelection.Count == 0)
                throw new ArgumentException("Rolling stock selection cannot be empty");
            if (!rollingStockSelection.OfType<LocomotiveModel>().Any())
                throw new InvalidOperationException("Train must have at least one locomotive");

            return new TrainConsistModel(trainName, rollingStockSelection);
        }
    }
}