using TMPro;
using TrainGame.Model.Service;
using UnityEngine;

namespace TrainGame.View.ServiceLedgerView
{
    public class ServiceLedgerItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI trainName;
        [SerializeField] private TextMeshProUGUI trainNo;
        [SerializeField] private TextMeshProUGUI departureStation;
        [SerializeField] private TextMeshProUGUI arrivalStation;
        [SerializeField] private TextMeshProUGUI passengerLoad;
        [SerializeField] private TextMeshProUGUI freightLoad;
        [SerializeField] private TextMeshProUGUI arrivalTime;
        
        private ServiceModel _serviceModel;
        
        public void Initialize(ServiceModel serviceModel)
        {
            _serviceModel = serviceModel;
            _serviceModel.OnModelChanged += RefreshView;
            RefreshView();
        }

        private void RefreshView()
        {
            trainName.text = _serviceModel.TrainConsist.name;
            trainNo.text = 133.ToString();
            departureStation.text = _serviceModel.RouteModel.departureStation.name;
            arrivalStation.text = _serviceModel.RouteModel.arrivalStation.name;
            passengerLoad.text = _serviceModel.TrainConsist.passengerLoad.ToString();
            freightLoad.text = _serviceModel.TrainConsist.freightLoad.ToString();
            arrivalTime.text = "14:11";
        }
    }
}