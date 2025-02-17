using System;
using Persistence;
using Train.Infrastructure;
using Train.Model;
using UnityEngine;

namespace Train
{
    public class RailwayDirector : PersistentSingleton<RailwayDirector>, ISaveable
    {
        private readonly ServiceManager _serviceManager = new ServiceManager();
        private TrainSaveManager _trainSaveManager;

        // public event Action OnTrainListUpdated
        // {
        //     add => TrainConsistRepository.OnTrainListUpdated += value;
        //     remove => TrainConsistRepository.OnTrainListUpdated -= value;
        // }
        
        // /// <summary>
        // /// Dispatches a train for service using the station's data.
        // /// </summary>
        // /// <param name="trainConsist">The train consist to dispatch.</param>
        // /// <param name="station">The station asset, which contains route and load rate info.</param>
        // /// <param name="masterType">Specifies whether this station handles loading (departing) or unloading (arriving).</param>
        // /// <param name="capacity">The capacity (load amount) to be processed.</param>
        // /// <returns>True if dispatch was successful.</returns>
        // public bool DispatchTrain(TrainConsistModel trainConsist, Station station, StationMasterType masterType, int capacity)
        // {
        //     if (_serviceManager.IsTrainInService(trainConsist))
        //     {
        //         Console.WriteLine($"Train {trainConsist.name} is already in service!");
        //         return false;
        //     }
        //     
        //     if (_serviceManager.CreateService(station, trainConsist, masterType, capacity))
        //     {
        //         TrainConsistRepository.PutTrainInService(trainConsist);
        //         return true;
        //     }
        //     return false;
        // }
        //
        // /// <summary>
        // /// Recalls a train from active service.
        // /// </summary>
        // /// <param name="trainConsist">The train consist to recall.</param>
        // /// <returns>True if recall was successful.</returns>
        // public bool RecallTrain(TrainConsistModel trainConsist)
        // {
        //     if (_serviceManager.FinishService(trainConsist))
        //     {
        //         TrainConsistRepository.RemoveTrainFromService(trainConsist);
        //         return true;
        //     }
        //     return false;
        // }
        //
        // public void AddTrainConsist(TrainConsistModel trainConsist) 
        //     => TrainConsistRepository.AddTrain(trainConsist);
        //
        // public void RemoveTrainConsist(TrainConsistModel trainConsist) 
        //     => TrainConsistRepository.RemoveTrain(trainConsist);
        //
        // public List<TrainConsistModel> GetAllTrainConsists() 
        //     => TrainConsistRepository.GetAllTrains();
        //
        // public List<TrainConsistModel> GetAllTrainConsists(ServiceStatus serviceStatus) 
        //     => TrainConsistRepository.GetAllTrains(serviceStatus);

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
            => TrainSaveManager.PopulateSaveData(TrainConsistRepository.I.GetAllTrains(), sd);

        public void LoadFromSaveData(SaveData sd)
        {
            TrainConsistRepository.I.Clear();
            TrainConsistRepository.I.AddTrains(TrainSaveManager.LoadFromSaveData(sd));
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