using System.Collections.Generic;
using Core.Utility;
using TrainGame.Controller;
using TrainGame.Model.Route;
using TrainGame.Model.Station;
using UnityEngine;

namespace TrainGame.View.JournalView
{
    public class JournalRouteView : MonoBehaviour
    {
        [SerializeField] private GameObject journalRouteItemPrefab;
        [SerializeField] private RectTransform contentContainerRouteAvailable;
        [SerializeField] private RectTransform contentContainerRouteNotAvailable;

        [SerializeField] private List<GameObject> journalRouteItems;
        
        public void Populate(JournalController journalController)
        {
            ClearContentContainers();
            
            foreach (var routeModel in RouteManager.I.QueryService.GetModels())
            {
                if(RouteManager.I.QueryService.GetRoutesByBuildState(BuildState.NotAvailableForBuilding).Contains(routeModel))
                    CreateContentItem(routeModel, journalController, contentContainerRouteNotAvailable);
                else
                {
                    CreateContentItem(routeModel, journalController, contentContainerRouteAvailable);
                }
            }
        }
        
        private void CreateContentItem(RouteModel routeModel, JournalController journalController, RectTransform parent)
        {
            var itemGo = Instantiate(journalRouteItemPrefab, parent);
            journalRouteItems.Add(itemGo);

            var itemUI = itemGo.GetComponent<JournalRouteItemView>();
            itemUI.Initialize(routeModel, journalController);
        }
        
        private void ClearContentContainers()
        {
            foreach (Transform child in contentContainerRouteAvailable)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in contentContainerRouteNotAvailable)
            {
                Destroy(child.gameObject);
            }
        }
    }
}