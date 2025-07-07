using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.CentralMenuView
{
    public class OpenMapHandler : MonoBehaviour
    {
        [SerializeField] private Button openMapButton;

        private void Start()
        {
            openMapButton.onClick.AddListener(() => UIStateManager.I.OnMapButtonClicked());
        }
    }
}