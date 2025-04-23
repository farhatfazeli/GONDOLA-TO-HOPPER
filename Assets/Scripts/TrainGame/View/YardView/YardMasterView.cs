using System.Collections.Generic;
using TrainGame.Controller;
using TrainGame.Model.RollingStock;
using UnityEngine;

namespace TrainGame.View.YardView
{
    public class YardMasterView : MonoBehaviour
    {
        private YardView _yardView;
        
        public void Initialize(YardMarshallController yardMarshallController, YardView yardView)
        {
            yardMarshallController.OnTrainConsistChanged += OnTrainConsistChanged;
            _yardView = yardView;
        }
        
        private void OnTrainConsistChanged(List<RollingStockModel> newConsist)
        {
            _yardView.UpdateMarshalling(newConsist);
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