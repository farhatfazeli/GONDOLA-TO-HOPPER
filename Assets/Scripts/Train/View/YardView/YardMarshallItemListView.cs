using System;
using System.Collections.Generic;
using Train.Controller;
using Train.Infrastructure;
using Train.Model.RollingStock;
using Train.Repositories;
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
        
        public void Populate()
        {
            
            // Clear existing items (optional)
            foreach (Transform child in contentContainer)
            {
                Destroy(child.gameObject);
            }

            // Create a UI entry for each RollingStockModel
            foreach (var rollingStockModel in RollingStockRepository.I.GetRollingStockModels())
            {
                var itemGo = Instantiate(rollingStockItemPrefab, contentContainer);
                rollingStockItems.Add(itemGo);
                // Suppose the prefab has a script that sets UI text/images
                var itemUI = itemGo.GetComponent<YardMarshallItemView>();
                itemUI.Initialize(yardMarshallController, rollingStockModel, RollingStockRepository.I.GetRollingStockSo(rollingStockModel));
            }
        }
    }
}