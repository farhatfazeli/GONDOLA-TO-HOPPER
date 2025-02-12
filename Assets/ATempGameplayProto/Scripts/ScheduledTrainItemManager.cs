using System;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;
using Button = UnityEngine.UI.Button;

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

    private Service _service;

    private Image _image;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    
    public void Initialize(Service service)
    {
        _service = service;

        routeName.text = _service.route.routeName;
        trainName.text = _service.train.name;
        loadInfo.text = $"Hauling {_service.loadAmount} of {_service.loadType}";
    }

    private void UpdateProgressSliders()
    {
        loadProgressSlider.value = _service.loadProgress.Value;
        travelProgressSlider.value = _service.travelProgress.Value;
        unloadProgressSlider.value = _service.unloadProgress.Value;
    }

    public void LoadTrain()
    {
        if (_service.isComplete) return;
        _service.ProgressLoadProgress(SO_GameParameters.I.loadClickProgressAmount);
        UpdateProgressSliders();

        if (_service.loadProgress.IsComplete)
        {
            loadButton.interactable = false;
            travelButton.interactable = true;
        }
    }

    public void TravelTrain()
    {
        if (_service.isComplete) return;
        if (_service.loadProgress.Value < 1) return;
        _service.ProgressTravelProgress(SO_GameParameters.I.travelClickProgressAmount);
        UpdateProgressSliders();

        if (_service.travelProgress.IsComplete)
        {
            travelButton.interactable = false;
            unloadButton.interactable = true;
        }
    }

    public void UnloadTrain()
    {
        if (_service.isComplete) return;
        if (_service.travelProgress.Value < 1) return;
        _service.ProgressUnloadProgress(SO_GameParameters.I.unloadClickProgressAmount);
        UpdateProgressSliders();

        if (_service.unloadProgress.IsComplete) unloadButton.interactable = false;

        if (_service.isComplete) _image.color = SO_GameParameters.I.achievedColor;
    }

    private void Update()
    {
        _service.Update();
        UpdateProgressSliders();
        if (_service.loadProgress.IsComplete)
        {
            loadButton.interactable = false;
            travelButton.interactable = true;
        }
        if (_service.travelProgress.IsComplete)
        {
            travelButton.interactable = false;
            unloadButton.interactable = true;
        }

        if (_service.unloadProgress.IsComplete)
            unloadButton.interactable = false;
        
        if (_service.isComplete)
            _image.color = SO_GameParameters.I.achievedColor;
        
    }
}