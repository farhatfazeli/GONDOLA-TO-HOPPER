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
        [SerializeField] private MapController mapController;
        [SerializeField] private RectTransform qrCodePanel;

        private void Start()
        {
            qrCodePanel.gameObject.SetActive(true);
        }


        public void GoToMainView()
        {
            mainView.gameObject.SetActive(true);
        }

        public void GoToMapView()
        {
            mapController.OnActivate();
        }
        
        public void CleanView()
        {
            mainView.gameObject.SetActive(false);
            mapController.OnDeactivate();
        }
    }
}