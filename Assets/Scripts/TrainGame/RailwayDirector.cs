using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence;
using ScriptableObjects;
using TrainGame.Infrastructure;
using TrainGame.Infrastructure.RollingStock;
using TrainGame.Infrastructure.Route;
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
            Task rollingStockLoadingTask = RollingStockLoader.LoadAllRollingStockModelsAsync(SO_GameParameters.I.addressableLabelRollingStock);

            Task routeLoadingTask = RouteLoader.LoadAllRouteModelsAsync(SO_GameParameters.I.addressableLabelRoutes);

            Task stationLoadingTask = StationLoader.LoadAllStationModelsAsync(SO_GameParameters.I.addressableLabelStations);

            await Task.WhenAll(rollingStockLoadingTask, routeLoadingTask, stationLoadingTask);

            IsInitialized = true;
        }

        private void Update()
        {
            UpdateGame(Time.deltaTime);
        }

        /// <summary>
        /// Update all active services. Call this once per frame.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the last update.</param>
        private static void UpdateGame(float deltaTime)
        {
            ServiceManager.I.UpdateServices(deltaTime);
            TimeManager.I.UpdateTime(deltaTime);
        }

        public void PopulateSaveData(SaveData sd)
        {
            RollingStockSaveHelper.PopulateSaveData(sd);
            RouteSaveHelper.PopulateSaveData(sd);
            StationSaveHelper.PopulateSaveData(sd);
            TimeSaveHelper.PopulateSaveData(sd);
            TrainConsistSaveHelper.PopulateSaveData(sd);
        }

        public void LoadFromSaveData(SaveData sd)
        {
            RollingStockSaveHelper.LoadFromSaveData(sd);
            RouteSaveHelper.LoadFromSaveData(sd);
            StationSaveHelper.LoadFromSaveData(sd);
            TimeSaveHelper.LoadFromSaveData(sd);
            TrainConsistSaveHelper.LoadFromSaveData(sd);
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