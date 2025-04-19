using System;
using TrainGame.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.CentralMenuView
{
    public class OpenStationTabHandler : MonoBehaviour
    {
        [SerializeField] private Button openStationTabButton;
        [SerializeField] private UIPanelController uiPanelController;

        private void Start()
        {
            openStationTabButton.onClick.AddListener(() => uiPanelController.GoToJournalStationView());
        }
    }
}