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

    private ScheduledTrainItem _scheduledTrainItem;

    private Image _image;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    
    public void Initialize(ScheduledTrainItem scheduledTrainItem)
    {
        _scheduledTrainItem = scheduledTrainItem;

        routeName.text = _scheduledTrainItem.route.routeName;
        trainName.text = _scheduledTrainItem.tempTrain.Name;
        loadInfo.text = $"Hauling {_scheduledTrainItem.loadAmount} of {_scheduledTrainItem.loadType}";
    }

    private void UpdateProgressSliders()
    {
        loadProgressSlider.value = _scheduledTrainItem.loadProgress.Value;
        travelProgressSlider.value = _scheduledTrainItem.travelProgress.Value;
        unloadProgressSlider.value = _scheduledTrainItem.unloadProgress.Value;
    }

    public void LoadTrain()
    {
        if (_scheduledTrainItem.isComplete) return;
        _scheduledTrainItem.ProgressLoadProgress(SO_GameParameters.I.loadClickProgressAmount);
        UpdateProgressSliders();

        if (_scheduledTrainItem.loadProgress.IsComplete)
        {
            loadButton.interactable = false;
            travelButton.interactable = true;
        }
    }

    public void TravelTrain()
    {
        if (_scheduledTrainItem.isComplete) return;
        if (_scheduledTrainItem.loadProgress.Value < 1) return;
        _scheduledTrainItem.ProgressTravelProgress(SO_GameParameters.I.travelClickProgressAmount);
        UpdateProgressSliders();

        if (_scheduledTrainItem.travelProgress.IsComplete)
        {
            travelButton.interactable = false;
            unloadButton.interactable = true;
        }
    }

    public void UnloadTrain()
    {
        if (_scheduledTrainItem.isComplete) return;
        if (_scheduledTrainItem.travelProgress.Value < 1) return;
        _scheduledTrainItem.ProgressUnloadProgress(SO_GameParameters.I.unloadClickProgressAmount);
        UpdateProgressSliders();

        if (_scheduledTrainItem.unloadProgress.IsComplete) unloadButton.interactable = false;

        if (_scheduledTrainItem.isComplete) _image.color = SO_GameParameters.I.achievedColor;
    }

    private void Update()
    {
        _scheduledTrainItem.Update();
        UpdateProgressSliders();
        if (_scheduledTrainItem.loadProgress.IsComplete)
        {
            loadButton.interactable = false;
            travelButton.interactable = true;
        }
        if (_scheduledTrainItem.travelProgress.IsComplete)
        {
            travelButton.interactable = false;
            unloadButton.interactable = true;
        }

        if (_scheduledTrainItem.unloadProgress.IsComplete)
            unloadButton.interactable = false;
        
        if (_scheduledTrainItem.isComplete)
            _image.color = SO_GameParameters.I.achievedColor;
        
    }
}