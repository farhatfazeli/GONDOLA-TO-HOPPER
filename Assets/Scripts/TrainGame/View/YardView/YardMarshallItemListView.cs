using System.Collections.Generic;
using ScriptableObjects;
using TrainGame.Controller;
using TrainGame.Model.RollingStock;
using UnityEngine;

namespace TrainGame.View.YardView
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
            foreach (var rollingStockModel in RollingStockRepository.I.GetModels())
            {
                var itemGo = Instantiate(rollingStockItemPrefab, contentContainer);
                rollingStockItems.Add(itemGo);
                // Suppose the prefab has a script that sets UI text/images
                var itemUI = itemGo.GetComponent<YardMarshallItemView>();
                itemUI.Initialize(yardMarshallController, rollingStockModel, RollingStockRepository.I.GetSo(rollingStockModel));
            }
        }
    }
}