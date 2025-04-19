using System.Collections.Generic;
using TrainGame.Controller;
using TrainGame.Model.RollingStock;
using TrainGame.Model.Station;
using TrainGame.View.YardView;
using UnityEngine;

namespace TrainGame.View.JournalView
{
    public class JournalStationView : MonoBehaviour
    {
        [SerializeField] private GameObject journalStationItemPrefab;
        [SerializeField] private RectTransform contentContainer;

        [SerializeField] private List<GameObject> journalStationItems;
        
        public void Populate(JournalController journalController)
        {
            ClearContentContainer();
            
            foreach (var stationModel in StationManager.I.QueryService.GetModels())
            {
                CreateContentItem(stationModel, journalController);
            }
        }
        
        private void CreateContentItem(StationModel stationModel, JournalController journalController)
        {
            var itemGo = Instantiate(journalStationItemPrefab, contentContainer);
            journalStationItems.Add(itemGo);

            var itemUI = itemGo.GetComponent<JournalStationItemView>();
            itemUI.Initialize(stationModel, journalController);
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