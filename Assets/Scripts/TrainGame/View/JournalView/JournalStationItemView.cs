using TMPro;
using TrainGame.Controller;
using TrainGame.Model.Station;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.JournalView
{
    public class JournalStationItemView
    {
        [SerializeField] private TextMeshProUGUI stationName;
        [SerializeField] private TextMeshProUGUI stationStatus;

        [SerializeField] private TextMeshProUGUI stationBuildPrice;
        [SerializeField] private TextMeshProUGUI stationBuildProgress;

        [SerializeField] private Button buildStationButton;
        
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
            stationStatus.text = _stationModel.stationBuilder.IsBuilt ? "Built" : "Not built";

            stationBuildPrice.text = "0";
            stationBuildProgress.text = _stationModel.stationBuilder.BuildProgress.ToString();
        }
    }
}