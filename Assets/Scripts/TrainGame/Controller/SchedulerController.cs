using System;
using TMPro;
using TrainGame.Model.Route;
using TrainGame.Model.Service;
using TrainGame.Model.TrainConsist;
using TrainGame.View.SchedulerView;
using UnityEngine;

namespace TrainGame.Controller
{
    public class SchedulerController : MonoBehaviour
    {
        [SerializeField] private RouteDropdownHandler routeDropdownHandler;
        
        [SerializeField] private TrainDropdownHandler trainDropdownHandler;
        
        [SerializeField] private ServiceLedgerController serviceLedgerController;

        public void ScheduleService()
        {
            RouteModel routeModel = routeDropdownHandler.GetSelectedRoute();
            TrainConsistModel trainConsistModel = trainDropdownHandler.GetSelectedTrain();
            
            ServiceModel serviceModel = ServiceManager.I.CreateService(routeModel, trainConsistModel);
            
            serviceLedgerController.AddScheduledService(serviceModel);
            
            UIStateManager.I.OnCloseWindowButtonClicked();
        }
    }
}