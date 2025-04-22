using System;
using System.Collections.Generic;
using System.Linq;
using Core.Persistence;
using TMPro;
using TrainGame.Model.Station;
using UnityEngine;

namespace TrainGame.View.SchedulerView
{
    public class DepartingStationDropdownHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown departingStationDropdown;
        
        private void RefreshView()
        {
            PopulateDropdown();
            SelectInitialRoute();
            UpdateDescription();
        }
        
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
            StationManager.I.OnStationDictionaryUpdated += RefreshView;
        }

        private void OnDisable()
        {
            StationManager.I.OnStationDictionaryUpdated -= RefreshView;
        }

        public StationModel GetSelectedStation()
        {
            if (departingStationDropdown.options.Count == 0)
                throw new FieldAccessException();

            string stationName = departingStationDropdown.options[departingStationDropdown.value].text;
            return StationManager.I.QueryService.GetStationModelByName(stationName);
        }

        private void SelectInitialRoute()
        {
            departingStationDropdown.value = 0;
        }

        public void UpdateDescription()
        {
            //routeDistance.text = $"<i>Distance: {GetSelectedRoute().distance / 1000:F1} km</i>";
        }

        private void PopulateDropdown()
        {
            departingStationDropdown.ClearOptions();
            
            AddNewRoutes(StationManager.I.QueryService.GetBuiltStations());
        }

        private void AddNewRoutes(IReadOnlyCollection<StationModel> stations)
        {
            departingStationDropdown.AddOptions(stations.Select(x => x.name).ToList());

            departingStationDropdown.RefreshShownValue();
        }
    }
}