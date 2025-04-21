using System.ComponentModel;
using Core.Utility;
using TMPro;
using TrainGame.Controller;
using TrainGame.Model.Route;
using TrainGame.Model.Station;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.JournalView
{
    public class JournalRouteItemView : MonoBehaviour
    {
        [SerializeField] private Image background;
        
        [SerializeField] private TextMeshProUGUI routeName;
        
        [Header("Build UI elements")]
        [SerializeField] private TextMeshProUGUI routeBuildState;
        [SerializeField] private TextMeshProUGUI routeBuildPrice;
        [SerializeField] private TextMeshProUGUI routeBuildProgress;

        [SerializeField] private Button buildRouteButton;
        
        [Header ("Visual options")]
        [SerializeField] private Color _unavailableColor;
        
        private RouteModel _routeModel;

        public void Initialize(RouteModel routeModel, JournalController journalController)
        {
            _routeModel = routeModel;
            _routeModel.OnModelChanged += RefreshView;
            RefreshView();
            
            buildRouteButton.onClick.AddListener(() => journalController.BuildRoute(_routeModel));
        }

        private void RefreshView()
        {
            routeName.text = _routeModel.name;

            HandleBuildState();

            routeBuildPrice.text = "0";
            routeBuildProgress.text = _routeModel.routeBuilder.BuildProgress.ToString();
        }

        private void HandleBuildState()
        {
            BuildState buildState = _routeModel.routeBuilder.GetRouteBuildState();
            SetBackground(buildState);
            routeBuildState.text = buildState switch
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
    }
}