using System;
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
        [SerializeField] private RectTransform plannerView;
        [SerializeField] private MapController mapController;
        [SerializeField] private RectTransform journalView;
        
        [SerializeField] private TabHandler landscapeTab;
        [SerializeField] private TabHandler yardTab;

        public void GoToMainView()
        {
            mainView.gameObject.SetActive(true);
        }
        
        public void GoToYardView()
        {
            yardMarshallController.OnActivate();
            landscapeTab.Deactivate();
        }
        
        public void GoToLandscapeView()
        {
            landscapeController.OnActivate();
            yardTab.Deactivate();
        }

        public void GoToPlannerView()
        {
            plannerView.gameObject.SetActive(true);
        }

        public void GoToMapView()
        {
            mapController.OnActivate();
        }

        public void GoToJournalView()
        {
            journalView.gameObject.SetActive(true);
        }
        
        public void CleanView()
        {
            mainView.gameObject.SetActive(false);
            yardMarshallController.OnDeactivate();
            landscapeController.OnDeactivate();
            plannerView.gameObject.SetActive(false);
            mapController.OnDeactivate();
            journalView.gameObject.SetActive(false);
        }
    }
}