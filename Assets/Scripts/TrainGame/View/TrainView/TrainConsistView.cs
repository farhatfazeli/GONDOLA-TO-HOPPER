using System.Collections.Generic;
using TrainGame.Model;
using UnityEngine;

namespace TrainGame.View.TrainView
{
    public class TrainConsistView : MonoBehaviour
    {
        private TrainConsistModel _trainConsistModel;
        private List<RollingStockView> _rollingStockViews;
        
        public void PopulateRollingStockViews(RollingStockView rollingStockView)
        {
            _rollingStockViews ??= new List<RollingStockView>();
            _rollingStockViews.Add(rollingStockView);
        }

        public TrainConsistView Construct(TrainConsistModel trainConsistModel)
        {
            _trainConsistModel = trainConsistModel;
            return this;
        }

        public Transform GetRearOfTrainConsist()
        {
            return _rollingStockViews[^1].rearCoupler;
        }
        
        public bool IsTrainConsistEmpty()
        {
            return _rollingStockViews == null || _rollingStockViews.Count == 0;
        }
    }
}