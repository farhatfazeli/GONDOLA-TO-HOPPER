using System;
using System.ComponentModel;
using Core.Utility;
using TMPro;
using TrainGame.Controller;
using TrainGame.Model.Station;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.JournalView
{
    public class JournalStationItemView : MonoBehaviour
    {
        [SerializeField] private Image background;
        
        [SerializeField] private TextMeshProUGUI stationName;
        
        [Header("Build UI elements")]
        [SerializeField] private TextMeshProUGUI stationBuildState;
        [SerializeField] private TextMeshProUGUI stationBuildPrice;
        [SerializeField] private TextMeshProUGUI stationBuildProgress;

        [Header("Interaction UI elements")]
        [SerializeField] private Button buildStationButton;
        
        [Header ("Visual options")]
        [SerializeField] private Color _unavailableColor;
        
        private StationModel _stationModel;

        public void Initialize(StationModel stationModel, JournalController journalController)
        {
            _stationModel = stationModel;
            _stationModel.OnModelChanged += RefreshView;
            RefreshView();
            
            buildStationButton.onClick.AddListener(() => journalController.BuildStation(_stationModel));
        }

        private void RefreshView()
        {
            stationName.text = _stationModel.name;

            HandleBuildState();

            stationBuildPrice.text = "0";
            stationBuildProgress.text = _stationModel.stationBuilder.BuildProgress.ToString();
        }

        private void HandleBuildState()
        {
            BuildState buildState = _stationModel.stationBuilder.GetStationBuildState();
            SetBackground(buildState);
            stationBuildState.text = buildState switch
            {
                BuildState.Built => "Built",
                BuildState.NotBuilt => "Not Built",
                BuildState.UnderConstruction => "Under Construction",
                BuildState.NotAvailableForBuilding => "Not Available For Building",
                _ => throw new InvalidEnumArgumentException(buildState.ToString(), (int)buildState, typeof(BuildState))
            };
        }

        private void SetBackground(BuildState buildState)
        {
            background.color = buildState switch
            {
                BuildState.NotAvailableForBuilding => _unavailableColor,
                _ => Color.white
            };
        }

        private void OnDestroy()
        {
            buildStationButton.onClick.RemoveAllListeners();
        }
    }
}