using System;
using Core.Persistence;
using Core.Utility;
using TrainGame.Controller;
using TrainGame.View.CentralMenuView;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrainGame
{
    public class UIStateManager : PersistentSingleton<UIStateManager>
    {
        private MainController mainController;
        private JournalController journalController;
        private ServiceDocketController serviceDocketController;
        private YardMarshallController yardMarshallController;
        private LandscapeController landscapeController;
        private MapController mapController;

        private UIState _previousState = UIState.None;
        private UIState _currentState = UIState.MainView;

        private ToggleTabViewEnum _toggleTabViewEnum = ToggleTabViewEnum.YardView;

        private void Start()
        {
            base.Awake();
            StartCoroutine(WaitAndLoad());
        }

        // private void OnEnable()
        // {
        //     SceneManager.sceneLoaded += OnSceneLoaded;
        // }
        //
        // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        // {
        //     if (scene.buildIndex == 1)
        //         StartCoroutine(WaitAndLoad());
        // }

        private System.Collections.IEnumerator WaitAndLoad()
        {
            // Wait until RailwayDirector is initialized.
            while (!RailwayDirector.I.IsInitialized || SceneManager.GetActiveScene() ==  SceneManager.GetSceneByBuildIndex(0))
                yield return null;
            
            Initialize();
        }

        private void Initialize()
        {
            mainController = FindFirstObjectByType<MainController>();
            journalController = FindFirstObjectByType<JournalController>();
            serviceDocketController = FindFirstObjectByType<ServiceDocketController>();
            yardMarshallController = FindFirstObjectByType<YardMarshallController>();
            landscapeController = FindFirstObjectByType<LandscapeController>();
            mapController = FindFirstObjectByType<MapController>();
            GoToState(UIState.YardView);
        }

        private void GoToState(UIState newState)
        {
            _previousState = _currentState;

            ExitPreviousState(_previousState, newState);
            
            // mainController.CleanView();
            
            switch (newState)
            {
                case UIState.MainView:
                    mainController.GoToMainView();
                    break;
                case UIState.YardView:
                    yardMarshallController.OnActivateView();
                    break;
                case UIState.LandscapeView:
                    landscapeController.OnActivateView();
                    break;
                case UIState.ServiceDocketView:
                    serviceDocketController.OnActivateView();
                    break;
                case UIState.MapView:
                    mapController.OnActivateView();
                    break;
                case UIState.JournalView:
                    journalController.OnActivateView();
                    break;
                case UIState.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            _currentState = newState;
        }

        private void ExitPreviousState(UIState previousState, UIState newState)
        {
            switch (previousState)
            {
                case UIState.LandscapeView:
                    if (newState == UIState.YardView)
                        landscapeController.OnDeactivateView();
                    break;
                case UIState.YardView:
                    if (newState == UIState.LandscapeView)
                    {
                        yardMarshallController.OnDeactivateView();
                    }
                    break;
                case UIState.MapView:
                    mapController.OnDeactivateView();
                    break;
                case UIState.ServiceDocketView:{}
                    serviceDocketController.OnDeactivateView();
                    yardMarshallController.OnDeactivateView();
                    break;
                case UIState.JournalView:
                    journalController.OnDeactivateView();
                    yardMarshallController.OnDeactivateView();
                    break;
                case UIState.None:
                    throw new ArgumentOutOfRangeException(nameof(previousState), previousState, null);
            }
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

        public void OnServiceDocketButtonClicked()
        {
            GoToState(UIState.ServiceDocketView);
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
        ServiceDocketView,
        MapView,
        JournalView
    }
}