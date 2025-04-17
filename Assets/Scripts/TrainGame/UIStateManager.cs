using System;
using Core.Persistence;
using Core.Utility;
using TrainGame.Controller;
using TrainGame.View.CentralMenuView;
using UnityEngine;

namespace TrainGame
{
    public class UIStateManager : PersistentSingleton<UIStateManager>
    {
        private UIPanelController panelController;

        private UIState _previousState = UIState.None;
        private UIState _currentState = UIState.None;

        private ToggleTabViewEnum _toggleTabViewEnum = ToggleTabViewEnum.YardView;

        private void Start()
        {
            base.Awake();
            StartCoroutine(WaitAndLoad());
        }
        
        private System.Collections.IEnumerator WaitAndLoad()
        {
            // Wait until RailwayDirector is initialized.
            while (!RailwayDirector.I.IsInitialized)
                yield return null;
            
            Initialize();
        }

        private void Initialize()
        {
            panelController = FindFirstObjectByType<UIPanelController>();
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

        public UIState GetUIState()
        {
            return _currentState;
        }

        public ToggleTabViewEnum GetToggleTabViewEnum()
        {
            return _toggleTabViewEnum;
        }

        public void OnYardLandscapeToggleClicked()
        {
            if (_currentState != UIState.YardView && _currentState != UIState.LandscapeView)
            {
                GoToState(GetToggleTabState());
            }
            else
            {
                GoToState(ToggleToggleTabState());
            }
        }

        public UIState ToggleToggleTabState()
        {
            switch (_toggleTabViewEnum)
            {
                case ToggleTabViewEnum.YardView:
                    _toggleTabViewEnum = ToggleTabViewEnum.LandscapeView;
                    return UIState.LandscapeView;
                case ToggleTabViewEnum.LandscapeView:
                    _toggleTabViewEnum = ToggleTabViewEnum.YardView;
                    return UIState.YardView;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public UIState GetToggleTabState()
        {
            switch (_toggleTabViewEnum)
            {
                case ToggleTabViewEnum.YardView:
                    return  UIState.YardView;
                case ToggleTabViewEnum.LandscapeView:
                    return  UIState.LandscapeView;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnYardButtonClicked()
        {
            GoToState(UIState.YardView);
        }

        public void OnLandscapeButtonClicked()
        {
            GoToState(UIState.LandscapeView);
        }

        public void OnSchedulerButtonClicked()
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