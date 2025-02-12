using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SchedulerManager : MonoBehaviour
{
    [Header("UI References")] public RouteDropdownManager routeDropdown;
    public TrainDropdownManager trainDropdown;
    public TMP_Dropdown loadTypeDropdown;
    public TMP_InputField loadAmountInput;
    public Transform scheduledTrainList; // Parent object for scheduled train UI entries

    public GameObject scheduledTrainItemPrefab;

    private List<ScheduledTrainItemManager> _scheduledTrainItemManagers = new();

    public void RefreshUI()
    {
        trainDropdown.RefreshUI();
    }
    
    public void ScheduleTrainItem()
    {
        var scheduledTrainItemManager = Instantiate(scheduledTrainItemPrefab, scheduledTrainList)
            .GetComponent<ScheduledTrainItemManager>();
        scheduledTrainItemManager.Initialize(MakeScheduledTrainItem());
    }

    private ScheduledTrainItem MakeScheduledTrainItem()
    {
        ScheduledTrainItem scheduledTrainItem = new(routeDropdown.selectedRoute, trainDropdown.selectedTrainObject,
            loadTypeDropdown.value == 0 ? LoadType.Passengers : LoadType.Cargo, ParseLoadAmount());

        return scheduledTrainItem;
    }

    private float ParseLoadAmount()
    {
        return float.TryParse(loadAmountInput.text, out float result) ? result : 0f;
    }
}