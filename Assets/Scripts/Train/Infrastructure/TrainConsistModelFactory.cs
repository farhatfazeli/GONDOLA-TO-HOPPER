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
        private readonly List<RollingStockModel> _trainConsistSelection = new();
        
        private bool _hasLocomotive = false;
        
        public void AddRollinStock(RollingStockModel rollingStockModel)
        {
            if(!_hasLocomotive)
                PerformFirstLocomotiveCheck();
            _trainConsistSelection.Add(rollingStockModel);
        }
        
        public List<RollingStockModel> GetTrainConsistSelection()
        {
            return _trainConsistSelection.ToList();
        }

        private void PerformFirstLocomotiveCheck()
        {
            if (_trainConsistSelection.First() is LocomotiveModel)
                _hasLocomotive = true;
            throw new InvalidOperationException("Train must have at least one locomotive");
        }

        public TrainConsistModel CreateTrainConsist(string trainName, List<RollingStockModel> rollingStockSelection)
        {
            if (rollingStockSelection == null || rollingStockSelection.Count == 0)
                throw new ArgumentException("Rolling stock selection cannot be empty");
            return new TrainConsistModel(trainName, rollingStockSelection);
        }
    }
}