using System;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;
using Button = UnityEngine.UI.Button;

public class ScheduledTrainItemManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI routeName;
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
    public void Initialize(ScheduledTrainItem scheduledTrainItem)
    {
        _scheduledTrainItem = scheduledTrainItem;
        
        routeName.text = _scheduledTrainItem.route.routeName;
        trainName.text = _scheduledTrainItem.tempTrain.trainName;
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
        if(_scheduledTrainItem.isComplete) return;
        _scheduledTrainItem.ProgressLoadProgress(0.5f);
        UpdateProgressSliders();
        
        if (_scheduledTrainItem.loadProgress.IsComplete)
        {
            loadButton.interactable = false;
        }
    }
    
    public void TravelTrain()
    {
        if(_scheduledTrainItem.isComplete) return;
        if(_scheduledTrainItem.loadProgress.Value < 1) return;
        _scheduledTrainItem.ProgressTravelProgress(0.5f);
        UpdateProgressSliders();
        
        if (_scheduledTrainItem.travelProgress.IsComplete)
        {
            travelButton.interactable = false;
        }
    }
    
    public void UnloadTrain()
    {
        if(_scheduledTrainItem.isComplete) return;
        if(_scheduledTrainItem.travelProgress.Value < 1) return;
        _scheduledTrainItem.ProgressUnloadProgress(0.5f);
        UpdateProgressSliders();
        
        if (_scheduledTrainItem.unloadProgress.IsComplete)
        {
            unloadButton.interactable = false;
        }

        if (_scheduledTrainItem.isComplete)
        {
            GetComponent<Image>().color = new Color32(0xF6, 0xFF, 0xAA, 0xFF);
        }
    }

    private void Update()
    {
        _scheduledTrainItem.Update();
    }
}
