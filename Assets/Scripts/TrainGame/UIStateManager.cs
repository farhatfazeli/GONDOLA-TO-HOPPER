using System;
using TrainGame.Controller;
using UnityEngine;

namespace TrainGame
{
    public class UIStateManager : MonoBehaviour
    {
        [SerializeField] private UIPanelController panelController;

        private UIState _previousState = UIState.None;
        private UIState _currentState = UIState.None;

        private void Awake()
        {
            GoToState(UIState.MainView);
        }

        private void GoToState(UIState newState)
        {
            _previousState = _currentState;
            
            panelController.CleanView();
            
            switch (newState)
            {
                case UIState.MainView:
                    panelController.GoToMainView();
                    break;
                case UIState.YardView:
                    panelController.GoToYardView();
                    break;
                case UIState.LandscapeView:
                    panelController.GoToLandscapeView();
                    break;
                case UIState.PlannerView:
                    panelController.GoToPlannerView();
                    break;
                case UIState.MapView:
                    panelController.GoToMapView();
                    break;
                case UIState.JournalView:
                    panelController.GoToJournalView();
                    break;
                case UIState.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            _currentState = newState;
        }

        public void OnYardButtonClicked()
        {
            GoToState(UIState.YardView);
        }

        public void OnLandscapeButtonClicked()
        {
            GoToState(UIState.LandscapeView);
        }

        public void OnPlannerButtonClicked()
        {
            GoToState(UIState.PlannerView);
        }

        public void OnMapButtonClicked()
        {
            GoToState(UIState.MapView);
        }

        public void OnJournalButtonClicked()
        {
            GoToState(UIState.JournalView);
        }

        public void OnCloseWindowButtonClicked()
        {
            GoToState(_previousState);
        }
    }

    public enum UIState
    {
        None,
        MainView,
        YardView,
        LandscapeView,
        PlannerView,
        MapView,
        JournalView
    }
}