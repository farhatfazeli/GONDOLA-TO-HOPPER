using System;
using TrainGame.View.CentralMenuView;
using TrainGame.View.UIView;
using UnityEngine;

namespace TrainGame.Controller
{
    public class UIPanelController : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private RectTransform mainView;
        [SerializeField] private YardMarshallController yardMarshallController;
        [SerializeField] private LandscapeController landscapeController;
        [SerializeField] private RectTransform schedulerView;
        [SerializeField] private MapController mapController;
        [SerializeField] private RectTransform journalBackground;
        [SerializeField] private RectTransform journalStationView;
        [SerializeField] private RectTransform journalRouteView;
        
        public void GoToMainView()
        {
            mainView.gameObject.SetActive(true);
        }
        
        public void GoToYardView()
        {
            yardMarshallController.OnActivate();
        }
        
        public void GoToLandscapeView()
        {
            landscapeController.OnActivate();
        }

        public void GoToPlannerView()
        {
            schedulerView.gameObject.SetActive(true);
        }

        public void GoToMapView()
        {
            mapController.OnActivate();
        }

        public void GoToJournalStationView()
        {
            journalRouteView.gameObject.SetActive(false);
            journalBackground.gameObject.SetActive(true);
            journalStationView.gameObject.SetActive(true);
        }
        
        public void GoToJournalRouteView()
        {
            journalStationView.gameObject.SetActive(false);
            journalRouteView.gameObject.SetActive(true);
            journalRouteView.gameObject.SetActive(true);
        }
        
        public void CleanView()
        {
            mainView.gameObject.SetActive(false);
            yardMarshallController.OnDeactivate();
            landscapeController.OnDeactivate();
            schedulerView.gameObject.SetActive(false);
            mapController.OnDeactivate();
            journalBackground.gameObject.SetActive(false);
            journalStationView.gameObject.SetActive(false);
            journalRouteView.gameObject.SetActive(false);
        }
    }
}