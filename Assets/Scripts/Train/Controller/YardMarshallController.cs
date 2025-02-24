using System;
using System.Collections.Generic;
using Train.Model.RollingStock;
using Train.Model.Yard;
using Train.Repositories;
using Train.View.YardView;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Train.Controller
{
    public class YardMarshallController : MonoBehaviour
    {
        [SerializeField] private YardMarshallItemListView yardMarshallItemListView;
        [SerializeField] private RectTransform yardView;
        
        public readonly YardMarshallModel yardMarshallModel = new();
        
        public event Action OnTrainConsistChanged;
        private void Start()
        {
            StartCoroutine(WaitAndDo());
        }

        private void OnEnable()
        {
            LoadYardScene();

        }

        private static void LoadYardScene()
        {
            SceneManager.LoadSceneAsync(SO_GameParameters.I.yardScene, LoadSceneMode.Additive);
        }

        private void OnDisable()
        {
            
        }

        private System.Collections.IEnumerator WaitAndDo()
        {
            // Wait until RailwayDirector is initialized.
            while (!RailwayDirector.I.IsInitialized || !SaveManager.I.IsLoadPhaseOver)
                yield return null;
            
            var rollingStock = RollingStockRepository.I.GetAllRollingStock();
            yardMarshallItemListView.Populate(rollingStock);
        }
        
        public void PurchaseRollingStock(RollingStockModel rollingStockModel)
        {

        }
        
        public void SelectRollingStock(RollingStockModel rollingStockModel)
        {
            yardMarshallModel.AddRollingStock(rollingStockModel);
            OnTrainConsistChanged?.Invoke();
        }

        public void ResetTrainConsist()
        {
            
        }

        public void ShowYardPanel()
        {
            
        }
    }
}