using System.Collections.Generic;
using Core.Utility;
using TrainGame.Controller;
using TrainGame.Model.Station;
using UnityEngine;

namespace TrainGame.View.JournalView
{
    public class JournalStationView : MonoBehaviour
    {
        [SerializeField] private GameObject journalStationItemPrefab;
        [SerializeField] private RectTransform contentContainerStationsAvailable;
        [SerializeField] private RectTransform contentContainerStationsNotAvailable;

        [SerializeField] private List<GameObject> journalStationItems;
        
        public void Populate(JournalController journalController)
        {
            ClearContentContainers();
            
            foreach (var stationModel in StationManager.I.QueryService.GetModels())
            {
                if(StationManager.I.QueryService.GetStationsByBuildState(BuildState.NotAvailableForBuilding).Contains(stationModel))
                    CreateContentItem(stationModel, journalController, contentContainerStationsNotAvailable);
                else
                {
                    CreateContentItem(stationModel, journalController, contentContainerStationsAvailable);
                }
            }
        }
        
        private void CreateContentItem(StationModel stationModel, JournalController journalController, RectTransform parent)
        {
            var itemGo = Instantiate(journalStationItemPrefab, parent);
            journalStationItems.Add(itemGo);

            var itemUI = itemGo.GetComponent<JournalStationItemView>();
            itemUI.Initialize(stationModel, journalController);
        }
        
        private void ClearContentContainers()
        {
            foreach (Transform child in contentContainerStationsAvailable)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in contentContainerStationsNotAvailable)
            {
                Destroy(child.gameObject);
            }
        }
    }
}