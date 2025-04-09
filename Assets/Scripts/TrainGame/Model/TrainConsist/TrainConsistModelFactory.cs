using System;
using System.Collections.Generic;
using System.Linq;
using TrainGame.Model.RollingStock;

namespace TrainGame.Model.TrainConsist
{
    public class TrainConsistModelFactory
    {
        private readonly List<RollingStockModel> _trainConsistSelection = new();
        
        private bool _hasLocomotive = false;
        
        public void AddRollinStock(RollingStockModel rollingStockModel)
        {
            if(!_hasLocomotive)
                PerformFirstLocomotiveCheck(rollingStockModel);
            _trainConsistSelection.Add(rollingStockModel);
        }
        
        public void RemoveRollingStock(int index)
        {
            if (_trainConsistSelection.Count == 0)
                throw new InvalidOperationException("Train consist is empty");
            _trainConsistSelection.RemoveAt(index);
        }
        
        public List<RollingStockModel> GetTrainConsistSelection()
        {
            return _trainConsistSelection.ToList();
        }

        private void PerformFirstLocomotiveCheck(RollingStockModel rollingStockModel)
        {
            if (rollingStockModel is LocomotiveModel)
                _hasLocomotive = true; 
            else
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