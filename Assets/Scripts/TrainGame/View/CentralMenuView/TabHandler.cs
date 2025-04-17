using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.UIView
{
    public class TabHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform activeTab;
        [SerializeField] private RectTransform inactiveTab;

        [SerializeField] private Button tabButton;

        private void Start()
        {
            tabButton.onClick.AddListener(() => UIStateManager.I.OnYardLandscapeToggleClicked());
        }

        public void OnTabClick()
        {
            activeTab.gameObject.SetActive(true);
            inactiveTab.gameObject.SetActive(false);
        }
        
        public void Deactivate()
        {
            activeTab.gameObject.SetActive(false);
            inactiveTab.gameObject.SetActive(true);
        }
    }
}