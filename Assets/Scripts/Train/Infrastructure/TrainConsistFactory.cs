using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Train.Model;

namespace Train.Infrastructure
{
    public class TrainConsistFactory
    {
        public static TrainConsistModel CreateTrainConsist(string trainName, List<RollingStock> rollingStockSelection)
        {
            if (rollingStockSelection == null || rollingStockSelection.Count == 0)
                throw new ArgumentException("Rolling stock selection cannot be empty");
            if (!rollingStockSelection.OfType<Locomotive>().Any())
                throw new InvalidOperationException("Train must have at least one locomotive");

            return new TrainConsistModel(trainName, rollingStockSelection);
        }
    }
}