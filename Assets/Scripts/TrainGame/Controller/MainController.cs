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
        [SerializeField] private MapController mapController;
        
        public void GoToMainView()
        {
            mainView.gameObject.SetActive(true);
        }
        
        public void GoToYardView()
        {
            yardMarshallController.OnActivateView();
        }
        
        public void GoToLandscapeView()
        {
            landscapeController.OnActivate();
        }

        public void GoToMapView()
        {
            mapController.OnActivate();
        }
        
        public void CleanView()
        {
            mainView.gameObject.SetActive(false);
            yardMarshallController.OnDeactivateView();
            landscapeController.OnDeactivate();
            mapController.OnDeactivate();
        }
    }
}