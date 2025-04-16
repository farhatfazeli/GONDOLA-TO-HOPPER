using System;
using System.Collections.Generic;
using System.Linq;
using Core.Persistence;
using TMPro;
using TrainGame.Model.Route;
using UnityEngine;
using UnityEngine.ResourceManagement.Exceptions;

namespace TrainGame.View.SchedulerView
{
    public class RouteDropdownHandler : MonoBehaviour
    {
        [Header("UI Elements")]
        public TMP_Dropdown routeDropdown;
        public TextMeshProUGUI routeDistance;

        private void Start()
        {
            StartCoroutine(WaitAndDo());
        }

        private System.Collections.IEnumerator WaitAndDo()
        {
            do
            {
                yield return null;
            } while (!RailwayDirector.I.IsInitialized || !SaveManager.I.IsLoadPhaseOver);

            Initialize();
        }

        private void Initialize()
        {
            RefreshView();
            RouteManager.I.OnRouteDictionaryUpdated += RefreshView;
        }

        private void RefreshView()
        {
            PopulateDropdown();
            SelectInitialRoute();
            UpdateDescription();
        }

        private void OnDisable()
        {
            RouteManager.I.OnRouteDictionaryUpdated -= RefreshView;
        }

        public RouteModel GetSelectedRoute()
        {
            if (routeDropdown.options.Count == 0)
                throw new FieldAccessException();

            string routeName = routeDropdown.options[routeDropdown.value].text;
            return RouteManager.I.QueryService.GetRouteModelByName(routeName);
        }

        private void SelectInitialRoute()
        {
            routeDropdown.value = 0;
        }

        public void UpdateDescription()
        {
            routeDistance.text = $"<i>Distance: {GetSelectedRoute().distance / 1000:F1} km</i>";
        }

        private void PopulateDropdown()
        {
            routeDropdown.ClearOptions();

            AddNewRoutes(RouteManager.I.QueryService.GetBuiltRoutes());
        }

        private void AddNewRoutes(IReadOnlyCollection<RouteModel> ros)
        {
            routeDropdown.AddOptions(ros.Select(x => x.name).ToList());
            
            routeDropdown.RefreshShownValue();
        }


        // public List<SO_Route> routes;
        //
        // public SO_Route selectedSoRoute;
        //
        // [Header("Infos")]
        // public TextMeshProUGUI distanceText;
        //
        // public void OnRouteSelected(int index)
        // {
        //     selectedSoRoute = routes[index];
        //     UpdateInfos();
        // }
        //
        // private void UpdateInfos()
        // {
        //     distanceText.text = $"<i>Distance: {selectedSoRoute.distance:F1} km</i>";
        // }
        //
        // private void Start()
        // {
        //     selectedSoRoute = routes[0];
        //     UpdateInfos();
        // }
    }
}