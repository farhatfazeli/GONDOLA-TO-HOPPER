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
        public List<RollingStockModel>
        
        public TrainConsistModel CreateTrainConsist(string trainName, List<SO_RollingStock> rollingStockSelection)
        {
            if (rollingStockSelection == null || rollingStockSelection.Count == 0)
                throw new ArgumentException("Rolling stock selection cannot be empty");
            if (!rollingStockSelection.OfType<SO_Locomotive>().Any())
                throw new InvalidOperationException("Train must have at least one locomotive");

            return new TrainConsistModel(trainName, rollingStockSelection);
        }
    }
}