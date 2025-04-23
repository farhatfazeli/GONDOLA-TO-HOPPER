using System;
using System.Collections.Generic;
using Core.Persistence;
using ScriptableObjects;
using TMPro;
using TrainGame.Model.Resource;
using TrainGame.Model.RollingStock;
using TrainGame.Model.Yard;
using TrainGame.View.YardView;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrainGame.Controller
{
    public enum YardMarshallFilter
    {
        Locomotives,
        PassengerWagons,
        FreightWagons
    }
    
    public class YardMarshallController : MonoBehaviour
    {
        [Header("Yard panels")]
        [SerializeField] private RectTransform yardViewPanel;
        [SerializeField] private RectTransform yardMarshallPanel;
        
        [Header("Yard Marshall list")]
        [SerializeField] private YardMarshallView yardMarshallView;

        [Header("Train consist details")]
        [SerializeField]private TMP_InputField trainNumberInput;
        [SerializeField]private TMP_InputField trainNameInput;
        
        private readonly YardMarshallModel _yardMarshallModel = new();
        
        public event Action<List<RollingStockModel>> OnTrainConsistChanged
        {
            add => _yardMarshallModel.OnTrainConsistChanged += value;
            remove => _yardMarshallModel.OnTrainConsistChanged -= value;
        }
        
        public void OnActivateView()
        {
            yardViewPanel.gameObject.SetActive(true);
            yardMarshallPanel.gameObject.SetActive(true);
            LoadYardScene();
            StartCoroutine(WaitAndDo());
        }
        
        public void OnDeactivateView()
        {
            yardViewPanel.gameObject.SetActive(false);
            yardMarshallPanel.gameObject.SetActive(false);
            UnloadYardScene();
        }

        private void LoadYardScene()
        {
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(SO_GameParameters.I.yardScene, LoadSceneMode.Additive);
            if (asyncOp != null) asyncOp.completed += operation => OnYardSceneLoaded();
        }
        
        private void UnloadYardScene()
        {
            Scene yardScene  = SceneManager.GetSceneByName(SO_GameParameters.I.yardScene);
            
            if (yardScene.isLoaded)
                SceneManager.UnloadSceneAsync(yardScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

        private System.Collections.IEnumerator WaitAndDo()
        {
            do
            {
                yield return null;
            } while (!RailwayDirector.I.IsInitialized || !SaveManager.I.IsLoadPhaseOver);
            
            PopulateView(YardMarshallFilter.Locomotives);
        }
        
        private void OnYardSceneLoaded()
        {
            YardMasterView yardMasterView = FindFirstObjectByType<YardMasterView>();
            YardView yardView = FindFirstObjectByType<YardView>();
            yardMasterView.Initialize(this, yardView);
        }

        public void PopulateView(YardMarshallFilter filter)
        {
            yardMarshallView.Populate(filter, this);
        }
        
        public void PurchaseRollingStock(RollingStockModel rollingStockModel)
        {
            if (ResourceManager.I.CheckResourceSpend(rollingStockModel.purchaseCost, ResourceType.Passengers))
            {
                ResourceManager.I.SpendResource(rollingStockModel.purchaseCost, ResourceType.Passengers);
                rollingStockModel.PurchaseRollingStock();
            }
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
    }
}