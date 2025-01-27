using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SchedulerManager : MonoBehaviour
{
    [Header("UI References")]
    public RouteDropdownManager routeDropdown;
    public TrainDropdownManager trainDropdown;
    public TMP_Dropdown loadTypeDropdown;
    public TMP_InputField loadAmountInput;
    public Transform scheduledTrainList; // Parent object for scheduled train UI entries

    public GameObject scheduledTrainItemPrefab;

    private List<ScheduledTrainItemManager> _scheduledTrainItemManagers = new();

    public void ScheduleTrainItem()
    {
        ScheduledTrainItemManager scheduledTrainItemManager = Instantiate(scheduledTrainItemPrefab, scheduledTrainList).GetComponent<ScheduledTrainItemManager>();
        scheduledTrainItemManager.Initialize(MakeScheduledTrainItem());
    }

    private ScheduledTrainItem MakeScheduledTrainItem()
    {
        ScheduledTrainItem scheduledTrainItem = new ScheduledTrainItem();
        scheduledTrainItem.route = routeDropdown.selectedRoute;
        scheduledTrainItem.tempTrain = trainDropdown.selectedTrain;
        scheduledTrainItem.loadType = loadTypeDropdown.value == 0 ? LoadType.Passengers : LoadType.Cargo;
        if (!float.TryParse(loadAmountInput.text, out scheduledTrainItem.loadAmount))
        {
            scheduledTrainItem.loadAmount = 0f; // Default to 0 if parsing fails
        }
        scheduledTrainItem.loadProgress = new Progress();
        scheduledTrainItem.travelProgress = new Progress();
        scheduledTrainItem.unloadProgress = new Progress();
        scheduledTrainItem.isComplete = false;
        return scheduledTrainItem;
    }
}