using System;
using System.Collections.Generic;
using Core.Persistence;
using TMPro;
using TrainGame;
using TrainGame.Model;
using TrainGame.Model.TrainConsist;
using UnityEngine;

public class TrainDropdownHandler : MonoBehaviour
{
    // [Header("UI Elements")] 
    // public TMP_Dropdown trainDropdown;
    // public TextMeshProUGUI tractionPowerText;
    // public TextMeshProUGUI maxSpeedText;
    //
    // private void Start()
    // {
    //     StartCoroutine(WaitAndDo());
    // }
    //
    // private System.Collections.IEnumerator WaitAndDo()
    // {
    //     do
    //     {
    //         yield return null;
    //     } while (!RailwayDirector.I.IsInitialized || !SaveManager.I.IsLoadPhaseOver);
    //
    //     TrainConsistRepository.I.OnTrainConsistListUpdated += RefreshUI;
    //     
    //     RefreshUI();
    // }
    //
    // private void OnDisable()
    // {
    //     TrainConsistRepository.I.OnTrainConsistListUpdated -= RefreshUI;
    // }
    //
    // public void RefreshUI()
    // {
    //     PopulateDropdown();
    // }
    //
    // private void PopulateDropdown()
    // {
    //     trainDropdown.ClearOptions();
    //
    //     var trains = TrainConsistRepository.I.GetAllTrainConsists();
    //     var trainNames = trains.ConvertAll(train => train.name);
    //
    //     trainDropdown.AddOptions(trainNames);
    //     trainDropdown.onValueChanged.RemoveAllListeners();
    //     trainDropdown.onValueChanged.AddListener(OnTrainSelected);
    //     
    //     SelectInitialTrain();
    // }
    //
    // private void SelectInitialTrain()
    // {
    //     List<TrainConsistModel> trains = TrainConsistRepository.I.GetAllTrainConsists();
    //
    //     if (trains.Count > 0)
    //     {
    //         OnTrainSelected(0); // Automatically selects the first train
    //         trainDropdown.value = 0;
    //     }
    // }
    //
    // public TrainConsistModel GetSelectedTrain()
    // {
    //     List<TrainConsistModel> trains = TrainConsistRepository.I.GetAllTrainConsists();
    //     if (trainDropdown.value < 0 || trainDropdown.value >= trains.Count) return null;
    //
    //     return trains[trainDropdown.value];
    // }
    //
    // public void OnTrainSelected(int index)
    // {
    //     List<TrainConsistModel> trains = TrainConsistRepository.I.GetAllTrainConsists();
    //     if (index < 0 || index >= trains.Count) return;
    //
    //     UpdateInfos(trains[index]);
    // }
    //
    // private void UpdateInfos(TrainConsistModel train)
    // {
    //     // tractionPowerText.text = $"<i>Traction power: {train.TractionCoefficient / 1000:F1} kN</i>";
    //     // maxSpeedText.text = $"<i>Max speed: {train.MaxSpeed:F1} km/h</i>";
    // }
}