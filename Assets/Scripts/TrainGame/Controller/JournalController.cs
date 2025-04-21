using System.Collections.Generic;
using System.Linq;
using TrainGame.Model.Route;
using TrainGame.Model.Station;
using TrainGame.View;
using TrainGame.View.JournalView;
using UnityEngine;

namespace TrainGame.Controller
{
    public class JournalController : MonoBehaviour
    {
        [SerializeField] private JournalPanelHandler journalPanelHandler;
        [SerializeField] private JournalStationView journalStationView;
        [SerializeField] private JournalRouteView journalRouteView;

        public void BuildStation(StationModel stationModel)
        {
            
        }

        public void BuildRoute(RouteModel routeModel)
        {
            
        }

        public void OnActivateView()
        {
            GoToStationView();
        }

        public void OnDeactivateView()
        {
            journalPanelHandler.CloseJournalView();
        }

        public void GoToStationView()
        {
            PopulateStationView();
            journalPanelHandler.GoToJournalStationView();
        }

        public void GoToRouteView()
        {
            PopulateRouteView();
            journalPanelHandler.GoToJournalRouteView();
        }

        private void PopulateStationView()
        {
            journalStationView.Populate(this);
        }

        private void PopulateRouteView()
        {
            journalRouteView.Populate(this);
        }
    }
}
