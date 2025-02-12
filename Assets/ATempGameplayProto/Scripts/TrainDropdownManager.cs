using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ScriptableObjects;
using TMPro;
using Train;
using Trains;
using UnityEngine;

public class TrainDropdownManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Dropdown trainDropdown;
    public TextMeshProUGUI tractionPowerText;
    public TextMeshProUGUI maxSpeedText;
    
    public TrainObject selectedTrainObject;

    private readonly List<TrainObject> _trains = new List<TrainObject>();

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        LoadTrains();
        PopulateDropdown();
        SelectInitialTrain();
    }
    
    private void LoadTrains()
    {
        _trains.Clear();
        _trains.AddRange(TrainController.Instance.trains);
    }
    
    private void PopulateDropdown()
    {
        trainDropdown.ClearOptions();

        List<string> trainNames = new List<string>();
        foreach (var train in _trains)
        {
            trainNames.Add(train.name);
        }

        trainDropdown.AddOptions(trainNames);
        trainDropdown.onValueChanged.AddListener(OnTrainSelected);
    }
    
    private void SelectInitialTrain()
    {
        if (_trains.Count > 0)
        {
            selectedTrainObject = _trains[0];
            trainDropdown.value = 0;
            UpdateInfos();
        }
    }

    public void OnTrainSelected(int index)
    {
        if (index < 0 || index >= _trains.Count) return;

        selectedTrainObject = _trains[index];
        UpdateInfos();
    }

    private void UpdateInfos()
    {
        if (selectedTrainObject == null) return;

        tractionPowerText.text = $"<i>Traction power: {selectedTrainObject.tractionCoefficient / 1000:F1} kN</i>";
        maxSpeedText.text = $"<i>Max speed: {selectedTrainObject.maxSpeed:F1} km/h</i>";
    }
}
