using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.CentralMenuView
{
    public class OpenSchedulerHandler : MonoBehaviour
    {
        [SerializeField] private Button openSchedulerButton;

        private void Start()
        {
            openSchedulerButton.onClick.AddListener(() => UIStateManager.I.OnSchedulerButtonClicked());
        }
    }
}