using System;
using System.Collections.Generic;
using Train.Controller;
using Train.Model.RollingStock;
using Train.Model.Yard;
using Train.View.WorldView;
using UnityEngine;

namespace Train.View.YardView
{
    public class YardView : MonoBehaviour
    {
        [SerializeField] private YardMarshallController yardMarshallController;

        private List<RollingStockView> _rollingStockViews;
        
        private YardMarshallModel _yardMarshallModel;
        
        private void OnEnable()
        {
            yardMarshallController = FindAnyObjectByType<YardMarshallController>();
            _yardMarshallModel = yardMarshallController.yardMarshallModel;
            yardMarshallController.OnTrainConsistChanged += OnTrainConsistChanged;
        }
        
        private void OnDisable()
        {
            yardMarshallController.OnTrainConsistChanged -= OnTrainConsistChanged;
        }

        private void OnTrainConsistChanged()
        {
            DisplayTrainConsist();
        }
        
        private void DisplayTrainConsist()
        {
            foreach (var rollingStockModel in _yardMarshallModel.TrainConsistSelection)
            {
                InstantiateRollingStock(rollingStockModel);
            }
        }

        private void InstantiateRollingStock(RollingStockModel rollingStockModel)
        {
            
        }
    }
}