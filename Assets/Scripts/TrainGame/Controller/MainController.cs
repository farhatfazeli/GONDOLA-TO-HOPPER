using System;
using TrainGame.View.CentralMenuView;
using TrainGame.View.UIView;
using UnityEngine;

namespace TrainGame.Controller
{
    public class MainController : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private RectTransform mainView;
        [SerializeField] private YardMarshallController yardMarshallController;
        [SerializeField] private LandscapeController landscapeController;
        [SerializeField] private RectTransform schedulerView;
        [SerializeField] private MapController mapController;
        
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
        
        public void CleanView()
        {
            mainView.gameObject.SetActive(false);
            yardMarshallController.OnDeactivate();
            landscapeController.OnDeactivate();
            schedulerView.gameObject.SetActive(false);
            mapController.OnDeactivate();
        }
    }
}