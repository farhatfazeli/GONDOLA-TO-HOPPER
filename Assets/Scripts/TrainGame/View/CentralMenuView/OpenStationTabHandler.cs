using System;
using TrainGame.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.CentralMenuView
{
    public class OpenStationTabHandler : MonoBehaviour
    {
        [SerializeField] private Button openStationTabButton;
        [SerializeField] private JournalController journalController;

        private void Start()
        {
            openStationTabButton.onClick.AddListener(() => journalController.GoToStationView());
        }
    }
}