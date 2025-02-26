using System;
using System.Collections.Generic;
using Persistence;
using ScriptableObjects;
using TMPro;
using TrainGame.Model.RollingStock;
using TrainGame.Model.Yard;
using TrainGame.View.YardView;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrainGame.Controller
{
    public class YardMarshallController : MonoBehaviour
    {
        [Header("Yard panel")]
        [SerializeField] private RectTransform yardPanel;
        
        [Header("Yard Marshall list")]
        [SerializeField] private YardMarshallItemListView yardMarshallItemListView;

        [Header("Train consist details")]
        [SerializeField]private TMP_InputField trainNumberInput;
        [SerializeField]private TMP_InputField trainNameInput;
        
        private readonly YardMarshallModel _yardMarshallModel = new();
        
        public event Action<List<RollingStockModel>> OnTrainConsistChanged
        {
            add => _yardMarshallModel.OnTrainConsistChanged += value;
            remove => _yardMarshallModel.OnTrainConsistChanged -= value;
        }
        
        public void OnActivate()
        {
            yardPanel.gameObject.SetActive(true);
            LoadYardScene();
            StartCoroutine(WaitAndDo());
        }
        
        public void OnDeactivate()
        {
            yardPanel.gameObject.SetActive(false);
            UnloadYardScene();
        }

        private void LoadYardScene()
        {
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(SO_GameParameters.I.yardScene, LoadSceneMode.Additive);
            if (asyncOp != null) asyncOp.completed += operation => OnYardSceneLoaded();
        }
        
        private void UnloadYardScene()
        {
            SceneManager.UnloadSceneAsync(SO_GameParameters.I.yardScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

        private System.Collections.IEnumerator WaitAndDo()
        {
            do
            {
                yield return null;
            } while (!RailwayDirector.I.IsInitialized || !SaveManager.I.IsLoadPhaseOver);
            
            yardMarshallItemListView.Populate();
            
        }
        
        private void OnYardSceneLoaded()
        {
            YardView yardView = FindFirstObjectByType<YardView>();
            YardMarshallView yardMarshallView = FindFirstObjectByType<YardMarshallView>();
            yardView.Initialize(this, yardMarshallView);
        }
        
        public void PurchaseRollingStock(RollingStockModel rollingStockModel)
        {

        }
        
        public void SelectRollingStock(RollingStockModel rollingStockModel)
        {
            _yardMarshallModel.AddRollingStock(rollingStockModel);
        }

        public void CreateTrainConsist()
        {
            string trainName = $"{trainNumberInput.text} {trainNameInput.text}";
            _yardMarshallModel.CreateTrainConsist(trainName);
        }
        
        public void Undo()
        {
            _yardMarshallModel.RemoveLastRollingStock();
        }

        public void ResetTrainConsist()
        {
            _yardMarshallModel.Reset();
        }

        public void ShowYardPanel()
        {
            
        }
    }
}