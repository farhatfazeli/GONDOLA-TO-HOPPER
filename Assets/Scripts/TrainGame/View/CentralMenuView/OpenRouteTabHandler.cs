using TrainGame.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.CentralMenuView
{
    public class OpenRouteTabHandler : MonoBehaviour
    {
        [SerializeField] private Button openRouteTabButton;
        [SerializeField] private JournalController journalController;


        private void Start()
        {
            openRouteTabButton.onClick.AddListener(() => journalController.GoToStationView());
        }
    }
}