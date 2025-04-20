using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.CentralMenuView
{
    public class OpenJournalHandler : MonoBehaviour
    {
        [SerializeField] private Button openJournalButton;

        private void Start()
        {
            openJournalButton.onClick.AddListener(() => UIStateManager.I.OnJournalButtonClicked());
        }
    }
}