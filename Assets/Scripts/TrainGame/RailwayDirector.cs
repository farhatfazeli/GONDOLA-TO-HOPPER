using System.Collections.Generic;
using Persistence;
using ScriptableObjects;
using TrainGame.Infrastructure;
using TrainGame.Infrastructure.RollingStock;
using TrainGame.Infrastructure.Station;
using TrainGame.Model.Station;
using TrainGame.Repositories;
using UnityEngine;
using Utility;

namespace TrainGame
{
    public class RailwayDirector : PersistentSingleton<RailwayDirector>, ISaveable
    {
        public bool IsInitialized { get; private set; }
        
        private async void Start()
        {
            List<StationModel> stations = await StationLoader.LoadAllStationModelsAsync(SO_GameParameters.I.addressableLabelStations);
            StationRepository.I.AddStations(stations);
            
            await RollingStockLoader.LoadAllRollingStockModelsAsync(SO_GameParameters.I.addressableLabelRollingStock);
            
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
        private static void UpdateServices(float deltaTime)
        {
            ServiceManager.I.UpdateServices(deltaTime);
            TimeManager.I.UpdateTime(deltaTime);
        }

        public void PopulateSaveData(SaveData sd)
        {
            TrainConsistSaveHelper.PopulateSaveData(sd);
            StationSaveHelper.PopulateSaveData(sd);
            RollingStockSaveHelper.PopulateSaveData(sd);
            TimeSaveHelper.PopulateSaveData(sd);
        }

        public void LoadFromSaveData(SaveData sd)
        {
            TrainConsistSaveHelper.LoadFromSaveData(sd);
            StationSaveHelper.LoadFromSaveData(sd);
            RollingStockSaveHelper.LoadFromSaveData(sd);
            TimeSaveHelper.LoadFromSaveData(sd);
        }
        
        public void Reset()
        {
            foreach (var train in TrainConsistRepository.I.GetAllTrainConsists())
            {
                train.Reset();
            }
            TrainConsistRepository.I.Clear();
        }
    }
}