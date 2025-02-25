using System;
using TMPro;
using TrainGame.Model;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;
using Button = UnityEngine.UI.Button;

public class ScheduledTrainItemManager : MonoBehaviour
{
    // [Header("UI References")] public TextMeshProUGUI routeName;
    // public TextMeshProUGUI trainName;
    // public TextMeshProUGUI loadInfo;
    // public TextMeshProUGUI haulRate;
    // public TextMeshProUGUI remainingTime;
    //
    // public Slider loadProgressSlider;
    // public Slider travelProgressSlider;
    // public Slider unloadProgressSlider;
    //
    // public Button loadButton;
    // public Button travelButton;
    // public Button unloadButton;
    //
    // private ServiceModel _serviceModel;
    //
    // private Image _image;
    //
    // private void Awake()
    // {
    //     _image = GetComponent<Image>();
    // }
    //
    // public void Initialize(ServiceModel serviceModel)
    // {
    //     _serviceModel = serviceModel;
    //
    //     routeName.text = _serviceModel.route.routeName;
    //     trainName.text = _serviceModel.train.name;
    //     loadInfo.text = $"Hauling {_serviceModel.loadAmount} of {_serviceModel.loadType}";
    // }
    //
    // private void UpdateProgressSliders()
    // {
    //     loadProgressSlider.value = _serviceModel.loadProgressTracker.Progress;
    //     travelProgressSlider.value = _serviceModel.travelProgressTracker.Progress;
    //     unloadProgressSlider.value = _serviceModel.unloadProgressTracker.Progress;
    // }
    //
    // public void LoadTrain()
    // {
    //     if (_serviceModel.IsComplete) return;
    //     _serviceModel.ProgressLoadProgress(SO_GameParameters.I.loadClickProgressAmount);
    //     UpdateProgressSliders();
    //
    //     if (_serviceModel.loadProgressTracker.IsFinished)
    //     {
    //         loadButton.interactable = false;
    //         travelButton.interactable = true;
    //     }
    // }
    //
    // public void TravelTrain()
    // {
    //     if (_serviceModel.IsComplete) return;
    //     if (_serviceModel.loadProgressTracker.Progress < 1) return;
    //     _serviceModel.ProgressTravelProgress(SO_GameParameters.I.travelClickProgressAmount);
    //     UpdateProgressSliders();
    //
    //     if (_serviceModel.travelProgressTracker.IsFinished)
    //     {
    //         travelButton.interactable = false;
    //         unloadButton.interactable = true;
    //     }
    // }
    //
    // public void UnloadTrain()
    // {
    //     if (_serviceModel.IsComplete) return;
    //     if (_serviceModel.travelProgressTracker.Progress < 1) return;
    //     _serviceModel.ProgressUnloadProgress(SO_GameParameters.I.unloadClickProgressAmount);
    //     UpdateProgressSliders();
    //
    //     if (_serviceModel.unloadProgressTracker.IsFinished) unloadButton.interactable = false;
    //
    //     if (_serviceModel.IsComplete) _image.color = SO_GameParameters.I.achievedColor;
    // }
    //
    // private void Update()
    // {
    //     _serviceModel.Update();
    //     UpdateProgressSliders();
    //     if (_serviceModel.loadProgressTracker.IsFinished)
    //     {
    //         loadButton.interactable = false;
    //         travelButton.interactable = true;
    //     }
    //     if (_serviceModel.travelProgressTracker.IsFinished)
    //     {
    //         travelButton.interactable = false;
    //         unloadButton.interactable = true;
    //     }
    //
    //     if (_serviceModel.unloadProgressTracker.IsFinished)
    //         unloadButton.interactable = false;
    //     
    //     if (_serviceModel.IsComplete)
    //         _image.color = SO_GameParameters.I.achievedColor;
    //     
    // }
}