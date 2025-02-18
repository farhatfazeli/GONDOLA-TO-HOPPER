using System;
using System.Collections.Generic;
using Persistence;
using Train.Infrastructure;
using Train.Infrastructure.RollingStock;
using Train.Infrastructure.Station;
using Train.Model;
using Train.Model.RollingStock;
using Train.Model.Station;
using Train.Repositories;
using UnityEngine;

namespace Train
{
    public class RailwayDirector : PersistentSingleton<RailwayDirector>, ISaveable
    {
        private readonly ServiceManager _serviceManager = new ServiceManager();
        
        public bool IsInitialized { get; private set; }
        
        private async void Start()
        {
            List<StationModel> stations = await StationLoader.LoadAllStationModelsAsync(SO_GameParameters.I.addressableLabelStations);
            StationRepository.I.AddStations(stations);
            List<RollingStockModel> rollingStock = await RollingStockLoader.LoadAllRollingStockModelsAsync(SO_GameParameters.I.addressableLabelRollingStock);
            RollingStockRepository.I.AddRollingStock(rollingStock);
            IsInitialized = true;
        }

        private void Update()
        {
            UpdateServices(Time.deltaTime);
        }

        /// <summary>
        /// Update all active services. Call this once per frame.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the last update.</param>
        public void UpdateServices(float deltaTime)
        {
            _serviceManager.UpdateServices(deltaTime);
        }

        public void PopulateSaveData(SaveData sd)
        {
            TrainSaveManager.PopulateSaveData(sd);
            StationSaveHelper.PopulateSaveData(sd);
            RollingStockSaveHelper.PopulateSaveData(sd);
        }

        public void LoadFromSaveData(SaveData sd)
        {
            TrainSaveManager.LoadFromSaveData(sd);
            StationSaveHelper.LoadFromSaveData(sd);
            RollingStockSaveHelper.LoadFromSaveData(sd);
        }
        
        public void Reset()
        {
            foreach (var train in TrainConsistRepository.I.GetAllTrains())
            {
                train.Reset();
            }
            TrainConsistRepository.I.Clear();
        }
    }
}