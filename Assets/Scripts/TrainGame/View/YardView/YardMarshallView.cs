using System.Collections.Generic;
using ScriptableObjects;
using TrainGame.Controller;
using TrainGame.Model.RollingStock;
using UnityEngine;

namespace TrainGame.View.YardView
{
    public class YardMarshallView : MonoBehaviour
    {
        [SerializeField] private GameObject rollingStockItemPrefab; // Your UI prefab
        [SerializeField] private RectTransform contentContainer;         // The Content under ScrollRect

        [SerializeField] private List<GameObject> rollingStockItems;
        
        private List<SO_RollingStock> _rollingStockAssets;
        
        public void Populate(YardMarshallFilter filter, YardMarshallController yardMarshallController)
        {
            ClearContentContainer();

            foreach (var rollingStockModel in RollingStockManager.I.QueryService.GetModels())
            {
                CreateContentItem(rollingStockModel,yardMarshallController, contentContainer);
            }
        }

        private void CreateContentItem(RollingStockModel rollingStockModel, YardMarshallController yardMarshallController, RectTransform parent)
        {
            var itemGo = Instantiate(rollingStockItemPrefab, contentContainer);
            rollingStockItems.Add(itemGo);
            // Suppose the prefab has a script that sets UI text/images
            var itemUI = itemGo.GetComponent<YardMarshallItemView>();
            itemUI.Initialize(yardMarshallController, rollingStockModel, RollingStockManager.I.QueryService.GetSo(rollingStockModel));
        }


        private void ClearContentContainer()
        {
            foreach (Transform child in contentContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}