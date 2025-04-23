using System;
using TrainGame.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.MainMenu
{
    public class PlayButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private MenuController menuController;

        private void Start()
        {
            playButton.onClick.AddListener(() =>
            {
                menuController.LoadUIScene();
                AudioManager.I.PlayTrainCrossing();
            });
        }
    }
}