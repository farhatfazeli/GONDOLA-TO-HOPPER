using UnityEngine;

namespace TrainGame.View.JournalView
{
    public class JournalPanelHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform journalBackground;
        [SerializeField] private RectTransform journalStationView;
        [SerializeField] private RectTransform journalRouteView;
        public void GoToJournalStationView()
        {
            journalRouteView.gameObject.SetActive(false);
            journalBackground.gameObject.SetActive(true);
            journalStationView.gameObject.SetActive(true);
        }
        
        public void GoToJournalRouteView()
        {
            journalStationView.gameObject.SetActive(false);
            journalRouteView.gameObject.SetActive(true);
            journalRouteView.gameObject.SetActive(true);
        }

        public void CloseJournalView()
        {
            journalBackground.gameObject.SetActive(false);
            journalStationView.gameObject.SetActive(false);
            journalRouteView.gameObject.SetActive(false);
        }
    }
}