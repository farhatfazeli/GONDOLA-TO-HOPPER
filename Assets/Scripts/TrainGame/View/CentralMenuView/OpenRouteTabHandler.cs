using TrainGame.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.CentralMenuView
{
    public class OpenRouteTabHandler : MonoBehaviour
    {
        [SerializeField] private Button openRouteTabButton;
        [SerializeField] private UIPanelController uiPanelController;


        private void Start()
        {
            openRouteTabButton.onClick.AddListener(() => uiPanelController.GoToJournalRouteView());
        }
    }
}