using System;
using System.Collections.Generic;
using ScriptableObjects;
using TrainGame.Controller;
using TrainGame.Model.RollingStock;
using UnityEngine;

namespace TrainGame.View.YardView
{
    public class YardMarshallView : MonoBehaviour
    {
        [SerializeField] private GameObject yardMarshallItemPrefab; // Your UI prefab
        [SerializeField] private RectTransform contentContainer;         // The Content under ScrollRect

        [SerializeField] private List<GameObject> yardMarshallItems;
        
        private List<SO_RollingStock> _rollingStockAssets;
        
        public void Populate(YardMarshallFilter filter, YardMarshallController yardMarshallController)
        {
            ClearContentContainer();

            switch (filter)
            {
                case YardMarshallFilter.Locomotives:
                    foreach (LocomotiveModel locomotiveModel in RollingStockManager.I.QueryService.GetLocomotiveModels())
                    {
                        CreateContentItem(locomotiveModel,yardMarshallController, contentContainer, filter);
                    }
                    break;
                case YardMarshallFilter.PassengerWagons:
                    foreach (WagonModel passengerWagonModel in RollingStockManager.I.QueryService.GetPassengerWagonModels())
                    {
                        CreateContentItem(passengerWagonModel,yardMarshallController, contentContainer, filter);
                    }
                    break;
                case YardMarshallFilter.FreightWagons:
                    foreach (WagonModel freightWagonModel in RollingStockManager.I.QueryService.GetFreightWagonModels())
                    {
                        CreateContentItem(freightWagonModel,yardMarshallController, contentContainer, filter);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, null);
            }
        }

        private void CreateContentItem(RollingStockModel rollingStockModel, YardMarshallController yardMarshallController, RectTransform parent, YardMarshallFilter filter)
        {
            var itemGo = Instantiate(yardMarshallItemPrefab, parent);
            yardMarshallItems.Add(itemGo);

            var itemUI = itemGo.GetComponent<YardMarshallItemView>();
            itemUI.Initialize(rollingStockModel, yardMarshallController, filter);
        }


        private void ClearContentContainer()
        {
            foreach (Transform child in contentContainer)
            {
                Destroy(child.gameObject);
            }
            
            yardMarshallItems.Clear();
        }
    }
}