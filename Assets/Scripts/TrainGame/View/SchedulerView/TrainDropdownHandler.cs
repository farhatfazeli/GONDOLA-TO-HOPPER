using System;
using System.Collections.Generic;
using System.Linq;
using Core.Persistence;
using TMPro;
using TrainGame.Model.Service;
using TrainGame.Model.TrainConsist;
using UnityEngine;

namespace TrainGame.View.SchedulerView
{
    public class TrainDropdownHandler : MonoBehaviour
    {
        [Header("UI Elements")] 
        public TMP_Dropdown trainDropdown;
        public TextMeshProUGUI tractionPowerText;
        public TextMeshProUGUI maxSpeedText;
    
        private void Start()
        {
            StartCoroutine(WaitAndDo());
        }
    
        private System.Collections.IEnumerator WaitAndDo()
        {
            do
            {
                yield return null;
            } while (!RailwayDirector.I.IsInitialized || !SaveManager.I.IsLoadPhaseOver);
        
            Initialize();
        }

        private void Initialize()
        {
            RefreshView();
            TrainConsistManager.I.OnTrainConsistListUpdated += RefreshView;
        }

        private void RefreshView()
        {
            PopulateDropdown();
            SelectInitialTrain();
            UpdateDescription();
        }
    
        private void OnDisable()
        {
            TrainConsistManager.I.OnTrainConsistListUpdated -= RefreshView;
        }
        
        public TrainConsistModel GetSelectedTrain()
        {
            if (trainDropdown.options.Count == 0)
                throw new FieldAccessException();
            
            string trainName = trainDropdown.options[trainDropdown.value].text;
            return TrainConsistManager.I.QueryService.GetTrainConsistByName(trainName);
        }
        
        private void SelectInitialTrain()
        {
            trainDropdown.value = 0;
        }

        private void UpdateDescription()
        {
            tractionPowerText.text = "400 HP";
            maxSpeedText.text = "100 KM/H";
        }
        
        private void PopulateDropdown()
        {
            trainDropdown.ClearOptions();
            
            AddNewTrains(ServiceManager.I.QueryService.GetTrainConsistsOnStandby());
        }

        private void AddNewTrains(IReadOnlyCollection<TrainConsistModel> trains)
        {
            trainDropdown.AddOptions(trains.Select(x =>  x.name).ToList());
            
            trainDropdown.RefreshShownValue();
        }
        
        private void UpdateInfos(TrainConsistModel train)
        {
            // tractionPowerText.text = $"<i>Traction power: {train.TractionCoefficient / 1000:F1} kN</i>";
            // maxSpeedText.text = $"<i>Max speed: {train.MaxSpeed:F1} km/h</i>";
        }
    }
}