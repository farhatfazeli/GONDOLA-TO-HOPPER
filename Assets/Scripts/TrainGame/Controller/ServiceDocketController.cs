using System;
using TMPro;
using TrainGame.Model.Route;
using TrainGame.Model.Service;
using TrainGame.Model.TrainConsist;
using TrainGame.View.SchedulerView;
using UnityEngine;

namespace TrainGame.Controller
{
    public class ServiceDocketController : MonoBehaviour
    {
        [SerializeField] private RectTransform serviceDocketView;

        [SerializeField] private RouteDropdownHandler routeDropdownHandler;
        [SerializeField] private DepartingStationDropdownHandler departingStationDropdownHandler;
        
        [SerializeField] private TrainDropdownHandler trainDropdownHandler;
        
        [SerializeField] private ServiceLedgerController serviceLedgerController;

        public void OnActivateView()
        {
            serviceDocketView.gameObject.SetActive(true);
        }

        public void OnDeactivateView()
        {
            serviceDocketView.gameObject.SetActive(false);
        } 

        public void ScheduleService()
        {
            RouteModel routeModel = routeDropdownHandler.GetSelectedRoute();
            TrainConsistModel trainConsistModel = trainDropdownHandler.GetSelectedTrain();
            
            ServiceModel serviceModel = ServiceManager.I.CreateService(routeModel, trainConsistModel);
            
            serviceLedgerController.AddScheduledService(serviceModel);
            
            UIStateManager.I.OnCloseWindowButtonClicked();
        }

        private void UpdateArrivalStationDropdown()
        {
            
        }
    }
}