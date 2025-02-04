using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ScriptableObjects;
using TMPro;
using UnityEngine;

public class TrainDropdownManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Dropdown trainDropdown;
    public TextMeshProUGUI tractionPowerText;
    public TextMeshProUGUI maxSpeedText;
    
    public TempTrain selectedTrain;

    private string _trainsFolderPath;
    private readonly List<TempTrain> _trains = new List<TempTrain>();

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        _trainsFolderPath = SO_GameParameters.I.trainsFolderPathShort;
        LoadTrains();
        PopulateDropdown();
        SelectInitialTrain();
    }
    
    private void LoadTrains()
    {
        _trains.Clear();
        TempTrain[] loadedTrains = Resources.LoadAll<TempTrain>(_trainsFolderPath);
        _trains.AddRange(loadedTrains.Where(train => !train.fileDeleted));

        if (_trains.Count == 0)
        {
            Debug.LogWarning("No train ScriptableObjects found in Resources/" + _trainsFolderPath);
        }
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
            selectedTrain = _trains[0];
            trainDropdown.value = 0;
            UpdateInfos();
        }
    }

    public void OnTrainSelected(int index)
    {
        if (index < 0 || index >= _trains.Count) return;

        selectedTrain = _trains[index];
        UpdateInfos();
    }

    private void UpdateInfos()
    {
        if (selectedTrain == null) return;

        tractionPowerText.text = $"<i>Traction power: {selectedTrain.tractionCoefficient / 1000:F1} kN</i>";
        maxSpeedText.text = $"<i>Max speed: {selectedTrain.maxSpeed:F1} km/h</i>";
    }
}
