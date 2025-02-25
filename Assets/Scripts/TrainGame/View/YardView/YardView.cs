using System.Collections.Generic;
using TrainGame.Controller;
using TrainGame.Model.RollingStock;
using UnityEngine;

namespace TrainGame.View.YardView
{
    public class YardView : MonoBehaviour
    {
        private YardMarshallView _yardMarshallView;
        
        public void Initialize(YardMarshallController yardMarshallController, YardMarshallView yardMarshallView)
        {
            yardMarshallController.OnTrainConsistChanged += OnTrainConsistChanged;
            _yardMarshallView = yardMarshallView;
        }
        
        private void OnTrainConsistChanged(List<RollingStockModel> newConsist)
        {
            _yardMarshallView.UpdateMarshalling(newConsist);
        }
        //
        // private void UpdateTrainConsistView()
        // {
        //     foreach (var rollingStockModel in _yardMarshallModel.TrainConsistSelection)
        //     {
        //         InstantiateRollingStock(rollingStockModel);
        //     }
        // }
        //
        // private void InstantiateRollingStock(RollingStockModel rollingStockModel)
        // {
        //     GameObject rollingStockViewPrefab = RollingStockRepository.I.GetRollingStockViewPrefab(rollingStockModel);
        //     var rollingStockView = Instantiate(rollingStockViewPrefab, transform).GetComponent<RollingStockView>();
        //     rollingStockView.Initialize(rollingStockModel);
        //     _rollingStockViews.Add(rollingStockView);
        // }
    }
}