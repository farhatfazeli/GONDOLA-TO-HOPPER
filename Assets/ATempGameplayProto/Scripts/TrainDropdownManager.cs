// using System;
// using System.Collections.Generic;
// using TMPro;
// using Train;
// using Train.Model;
// using UnityEngine;
//
// public class TrainDropdownManager : MonoBehaviour
// {
//     [Header("UI Elements")] 
//     public TMP_Dropdown trainDropdown;
//     public TextMeshProUGUI tractionPowerText;
//     public TextMeshProUGUI maxSpeedText;
//
//     private TrainController _trainController;
//     
//     private void Awake()
//     {
//         _trainController = TrainController.Instance;
//     }
//
//     private void OnEnable()
//     {
//         _trainController.OnTrainListUpdated += RefreshUI;
//     }
//     
//     private void OnDisable()
//     {
//         _trainController.OnTrainListUpdated -= RefreshUI;
//     }
//
//     private void Start()
//     {
//         RefreshUI();
//     }
//
//     public void RefreshUI()
//     {
//         PopulateDropdown();
//         SelectInitialTrain();
//     }
//     
//     private void PopulateDropdown()
//     {
//         trainDropdown.ClearOptions();
//
//         List<TrainConsistModel> trains = _trainController.GetAllTrainConsists();
//         var trainNames = trains.ConvertAll(train => train.name);
//
//         trainDropdown.AddOptions(trainNames);
//         trainDropdown.onValueChanged.RemoveAllListeners();
//         trainDropdown.onValueChanged.AddListener(OnTrainSelected);
//     }
//
//     private void SelectInitialTrain()
//     {
//         List<TrainConsistModel> trains = _trainController.GetAllTrainConsists();
//
//         if (trains.Count > 0)
//         {
//             OnTrainSelected(0); // Automatically selects the first train
//             trainDropdown.value = 0;
//         }
//     }
//
//     public void OnTrainSelected(int index)
//     {
//         List<TrainConsistModel> trains = _trainController.GetAllTrainConsists();
//         if (index < 0 || index >= trains.Count) return;
//
//         UpdateInfos(trains[index]);
//     }
//
//     private void UpdateInfos(TrainConsistModel train)
//     {
//         // tractionPowerText.text = $"<i>Traction power: {train.TractionCoefficient / 1000:F1} kN</i>";
//         // maxSpeedText.text = $"<i>Max speed: {train.MaxSpeed:F1} km/h</i>";
//     }
// }