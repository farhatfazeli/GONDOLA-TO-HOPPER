using ScriptableObjects;
using TMPro;
using TrainGame.Model.RollingStock;
using TrainGame.Model.Service;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.ServiceLedgerView
{
    public class ScheduledTrainItemManager : MonoBehaviour
    {
        [Header("UI References")] public TextMeshProUGUI routeName;
        public TextMeshProUGUI trainName;
        public TextMeshProUGUI loadInfo;
        public TextMeshProUGUI haulRate;
        public TextMeshProUGUI remainingTime;
        
        public Slider loadProgressSlider;
        public Slider travelProgressSlider;
        public Slider unloadProgressSlider;
        
        public Button loadButton;
        public Button travelButton;
        public Button unloadButton;
        
        private ServiceModel _serviceModel;
        
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }
        
        public void Initialize(ServiceModel serviceModel)
        {
            _serviceModel = serviceModel;

            routeName.text = _serviceModel.RouteModel.name;
            trainName.text = _serviceModel.TrainConsist.name;
            loadInfo.text = $"Hauling {_serviceModel.TrainConsist.passengerLoad} of {LoadType.Passengers}";
        }
        
        private void UpdateProgressSliders()
        {
            loadProgressSlider.value = _serviceModel.DepartureStationMasterModel._passengerProgressTracker.Progress;
            travelProgressSlider.value = _serviceModel.trainDriver._travelProgress.Progress;
            unloadProgressSlider.value = _serviceModel.ArrivalStationMasterModel._passengerProgressTracker.Progress;
        }
        
        public void LoadTrain()
        {
            if (_serviceModel.IsComplete) return;
            _serviceModel.DepartureStationMasterModel._passengerProgressTracker.AdvanceBy(SO_GameParameters.I.loadClickProgressAmount);
            UpdateProgressSliders();
        
            if (_serviceModel.DepartureStationMasterModel._passengerProgressTracker.IsFinished)
            {
                loadButton.interactable = false;
                travelButton.interactable = true;
            }
        }
        
        public void TravelTrain()
        {
            // if (_serviceModel.IsComplete) return;
            // if (_serviceModel.loadProgressTracker.Progress < 1) return;
            // _serviceModel.ProgressTravelProgress(SO_GameParameters.I.travelClickProgressAmount);
            // UpdateProgressSliders();
            //
            // if (_serviceModel.travelProgressTracker.IsFinished)
            // {
            //     travelButton.interactable = false;
            //     unloadButton.interactable = true;
            // }
        }
        
        public void UnloadTrain()
        {
            // if (_serviceModel.IsComplete) return;
            // if (_serviceModel.travelProgressTracker.Progress < 1) return;
            // _serviceModel.ProgressUnloadProgress(SO_GameParameters.I.unloadClickProgressAmount);
            // UpdateProgressSliders();
            //
            // if (_serviceModel.unloadProgressTracker.IsFinished) unloadButton.interactable = false;
            //
            // if (_serviceModel.IsComplete) _image.color = SO_GameParameters.I.achievedColor;
        }
        
        private void Update()
        {
            UpdateProgressSliders();
            if (_serviceModel.DepartureStationMasterModel._passengerProgressTracker.IsFinished)
            {
                loadButton.interactable = false;
                travelButton.interactable = true;
            }
            if (_serviceModel.trainDriver._travelProgress.IsFinished)
            {
                travelButton.interactable = false;
                unloadButton.interactable = true;
            }
        
            if (_serviceModel.ArrivalStationMasterModel._passengerProgressTracker.IsFinished)
                unloadButton.interactable = false;
            
            if (_serviceModel.IsComplete)
                _image.color = SO_GameParameters.I.achievedColor;
            
        }
    }
}