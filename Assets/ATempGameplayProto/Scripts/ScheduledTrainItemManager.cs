using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class ScheduledTrainItemManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI routeName;
    public TextMeshProUGUI trainName;
    public TextMeshProUGUI loadInfo;
    public TextMeshProUGUI haulRate;
    public TextMeshProUGUI remainingTime;
    
    public Slider loadProgress;
    
    private ScheduledTrainItem _scheduledTrainItem;
    public void Initialize(ScheduledTrainItem scheduledTrainItem)
    {
        _scheduledTrainItem = scheduledTrainItem;
        
        routeName.text = _scheduledTrainItem.route.routeName;
        trainName.text = _scheduledTrainItem.tempTrain.trainName;
        loadInfo.text = $"Hauling {_scheduledTrainItem.loadAmount} of {_scheduledTrainItem.loadType}";
    }

    public void UpdateLoadProgress()
    {
        loadProgress.value += 0.1f;
    }
    
}
