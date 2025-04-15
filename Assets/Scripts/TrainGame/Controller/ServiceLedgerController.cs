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
            serviceLedgerView.AddServiceViewItem(serviceModel);
        }
    }
}