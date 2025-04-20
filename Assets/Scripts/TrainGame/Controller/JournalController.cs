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
        public void BuildStation(StationModel stationModel)
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
            journalPanelHandler.GoToJournalRouteView();
        }

        private void PopulateStationView()
        {
            journalStationView.Populate(this);
        }
    }
}
