using System.Collections.Generic;
using Train.Infrastructure;
using Train.Model.RollingStock;

namespace Train.Model.Yard
{
    public class YardMarshallModel
    {
        private TrainConsistModelFactory _trainConsistModelFactory = new TrainConsistModelFactory();
        
        public List<RollingStockModel> TrainConsistSelection => _trainConsistModelFactory.GetTrainConsistSelection();
        
        public void AddRollingStock(RollingStockModel rollingStockModel)
        {
            _trainConsistModelFactory.AddRollinStock(rollingStockModel);
        }
    }
}