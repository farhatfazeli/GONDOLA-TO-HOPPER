using System;
using System.Collections.Generic;
using Train.Controller;
using Train.Model.RollingStock;
using UnityEngine;
using Utility;

namespace Train.View.YardView
{
    public class YardMarshallItemListView : MonoBehaviour
    {
        [SerializeField] private YardMarshallController yardMarshallController;
        [SerializeField] private GameObject rollingStockItemPrefab; // Your UI prefab
        [SerializeField] private Transform contentContainer;         // The Content under ScrollRect

        [SerializeField] private List<GameObject> rollingStockItems;
        
        private List<SO_RollingStock> _rollingStockAssets;
        private bool _isInitialized;
        private async void Start()
        {
            _rollingStockAssets = await AddressableLoader<SO_RollingStock>.LoadAllAsync(SO_GameParameters.I.addressableLabelRollingStock);
            _isInitialized = true;
        }

        public void Populate(List<RollingStockModel> rollingStockModels)
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("YardMarshallItemListView is not initialized yet!");
            }
            
            // Clear existing items (optional)
            foreach (Transform child in contentContainer)
            {
                Destroy(child.gameObject);
            }

            // Create a UI entry for each RollingStockModel
            foreach (var rollingStockModel in rollingStockModels)
            {
                var itemGo = Instantiate(rollingStockItemPrefab, contentContainer);
                rollingStockItems.Add(itemGo);
                // Suppose the prefab has a script that sets UI text/images
                var itemUI = itemGo.GetComponent<YardMarshallItemView>();
                itemUI.Initialize(yardMarshallController, rollingStockModel, FindSO_RollingStock(rollingStockModel));
            }
        }
        
        private SO_RollingStock FindSO_RollingStock(RollingStockModel rollingStockModel)
        {
            return _rollingStockAssets.Find(r => r.uuid == rollingStockModel.uuid);
        }
        
    }
}