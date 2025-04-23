using System;
using TMPro;
using TrainGame.Model.Route;
using TrainGame.Model.Service;
using TrainGame.Model.Station;
using TrainGame.Model.TrainConsist;
using TrainGame.View.SchedulerView;
using UnityEngine;

namespace TrainGame.Controller
{
    public class ServiceDocketController : MonoBehaviour
    {
        [SerializeField] private RectTransform serviceDocketView;
        
        [SerializeField] private DepartingStationDropdownHandler departingStationDropdownHandler;
        [SerializeField] private ArrivalStationDropdownHandler arrivalStationDropdownHandler;
        
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

            
            TrainConsistModel trainConsistModel = trainDropdownHandler.GetSelectedTrain();
            
            ServiceModel serviceModel = ServiceManager.I.CreateService(GetSelectedRoute(), trainConsistModel);
            
            serviceLedgerController.AddScheduledService(serviceModel);
            
            UIStateManager.I.OnCloseWindowButtonClicked();
        }

        private RouteModel GetSelectedRoute()
        {
            StationModel departureStation = departingStationDropdownHandler.GetSelectedStation();
            StationModel arrivalStation = arrivalStationDropdownHandler.GetSelectedStation();
            
            return RouteManager.I.QueryService.GetRouteBetweenStations(departureStation, arrivalStation);
        }
    }
}