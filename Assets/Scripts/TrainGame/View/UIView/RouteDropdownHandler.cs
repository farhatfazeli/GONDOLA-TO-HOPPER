using System.Collections.Generic;
using System.Linq;
using Core.Persistence;
using TMPro;
using TrainGame.Model.Route;
using UnityEngine;

namespace TrainGame.View.UIView
{
    public class RouteDropdownHandler : MonoBehaviour
    {
        [Header("UI Elements")] public TMP_Dropdown routeDropdown;
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
            PopulateDropdown();
            RouteManager.I.OnRouteDictionaryUpdated += PopulateDropdown;

            SelectInitialRoute();
            UpdateDescription();
        }

        private void OnDisable()
        {
            RouteManager.I.OnRouteDictionaryUpdated -= PopulateDropdown;
        }

        public RouteModel GetSelectedRoute()
        {
            if (routeDropdown.options.Count < 0)
                return null;

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

        private void AddNewRoutes(List<RouteModel> ros)
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