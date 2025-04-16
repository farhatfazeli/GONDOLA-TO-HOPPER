using System;
using TrainGame.Model.Service;
using TrainGame.View.ServiceLedgerView;
using UnityEngine;

namespace TrainGame.Controller
{
    public class ServiceLedgerController : MonoBehaviour
    {
        [SerializeField] private ServiceLedgerView serviceLedgerView;

        public void AddScheduledService(ServiceModel serviceModel)
        {
            if (serviceLedgerView == null)
                throw new ArgumentNullException(serviceModel.name, "Service model is null");
            
            serviceLedgerView.AddServiceViewItem(serviceModel);
        }
    }
}