using System;
using System.Collections.Generic;
using System.Linq;
using Core.Persistence;
using Core.Utility;
using TMPro;
using TrainGame.Model.Station;
using UnityEngine;

namespace TrainGame.View.SchedulerView
{
    public class ArrivalStationDropdownHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown arrivalStationDropdown;
        [SerializeField] private DepartingStationDropdownHandler departingStationDropdownHandler;
        
        public void RefreshView()
        {
            PopulateDropdown();
            SelectInitialRoute();
            UpdateDescription();
        }
        
        // private void Start()
        // {
        //     StartCoroutine(WaitAndDo());
        // }
        //
        // private System.Collections.IEnumerator WaitAndDo()
        // {
        //     do
        //     {
        //         yield return null;
        //     } while (!RailwayDirector.I.IsInitialized || !SaveManager.I.IsLoadPhaseOver);
        //
        //     Initialize();
        // }
        //
        // private void Initialize()
        // {
        //     RefreshView();
        //     StationManager.I.OnStationDictionaryUpdated += RefreshView;
        // }
        //
        // private void OnDisable()
        // {
        //     StationManager.I.OnStationDictionaryUpdated -= RefreshView;
        // }

        public StationModel GetSelectedStation()
        {
            if (arrivalStationDropdown.options.Count == 0)
                throw new FieldAccessException();

            string stationName = arrivalStationDropdown.options[arrivalStationDropdown.value].text;
            return StationManager.I.QueryService.GetStationModelByName(stationName);
        }

        private void SelectInitialRoute()
        {
            arrivalStationDropdown.value = 0;
        }

        public void UpdateDescription()
        {
            //routeDistance.text = $"<i>Distance: {GetSelectedRoute().distance / 1000:F1} km</i>";
        }

        private void PopulateDropdown()
        {
            Debug.Log("Populating arrival dropdown");
            arrivalStationDropdown.ClearOptions();

            StationModel departureStation = departingStationDropdownHandler.GetSelectedStation();
            
            AddNewRoutes(StationManager.I.QueryService.GetConnectedStations(departureStation, BuildState.Built));
        }

        private void AddNewRoutes(IReadOnlyCollection<StationModel> stations)
        {
            arrivalStationDropdown.AddOptions(stations.Select(x => x.name).ToList());

            arrivalStationDropdown.RefreshShownValue();
        }
    }
}