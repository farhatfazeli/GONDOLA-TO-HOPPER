using TMPro;
using TrainGame.Model.Service;
using UnityEngine;

namespace TrainGame.View.ServiceLedgerView
{
    public class ServiceLedgerItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI trainNo;
        [SerializeField] private TextMeshProUGUI trainName;
        [SerializeField] private TextMeshProUGUI departureStation;
        [SerializeField] private TextMeshProUGUI arrivalStation;
        [SerializeField] private TextMeshProUGUI load;
        [SerializeField] private TextMeshProUGUI arrivalTime;
        [SerializeField] private TextMeshProUGUI status;
        
        //temp code
        [SerializeField] private ScheduledTrainItemManager _scheduledTrainItemManager;
        
        private ServiceModel _serviceModel;
        
        public void Initialize(ServiceModel serviceModel)
        {
            _serviceModel = serviceModel;
            _serviceModel.OnModelChanged += RefreshView;
            RefreshView();
            
            // temp code
             _scheduledTrainItemManager.Initialize(serviceModel);
        }

        private void RefreshView()
        {
            trainNo.text = 133.ToString();
            trainName.text = _serviceModel.serviceInfo.TrainConsist.name;
            departureStation.text = _serviceModel.serviceInfo.RouteModel.departureStation.name;
            arrivalStation.text = _serviceModel.serviceInfo.RouteModel.arrivalStation.name;
            load.text = "Mixed";
            arrivalTime.text = "14:11";
            status.text = _serviceModel.ServiceStatus.ToString();
        }
    }
}