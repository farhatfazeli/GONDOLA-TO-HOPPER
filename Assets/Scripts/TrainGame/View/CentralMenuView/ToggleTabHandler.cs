using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.CentralMenuView
{
    public enum ToggleTabViewEnum
    {
        LandscapeView,
        YardView
    }
    
    public class ToggleTabHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform landscapeIcon;
        [SerializeField] private RectTransform yardIcon;

        [SerializeField] private Button tabButton;

        private void Start()
        {
            tabButton.onClick.AddListener(() =>
            {
                PlayTravelTheme();
                UIStateManager.I.OnYardLandscapeToggleClicked();
                RefreshView();
            });
        }

        private void PlayTravelTheme()
        {
            AudioManager.I.PlayTravelTheme();
        }

        private void RefreshView()
        {
            switch (UIStateManager.I.GetToggleTabViewEnum())
            {
                case ToggleTabViewEnum.LandscapeView:
                    ShowYardIcon();
                    break;
                case ToggleTabViewEnum.YardView:
                    ShowLandscapeIcon();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ShowLandscapeIcon()
        {
            landscapeIcon.gameObject.SetActive(true);
            yardIcon.gameObject.SetActive(false);
        }
        
        public void ShowYardIcon()
        {
            yardIcon.gameObject.SetActive(true);
            landscapeIcon.gameObject.SetActive(false);
        }
    }
}