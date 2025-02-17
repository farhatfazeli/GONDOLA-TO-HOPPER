// using System;
// using System.Collections.Generic;
// using Persistence;
// using ScriptableObjects;
// using Train.Infrastructure;
// using Train.Model;
//
// namespace Train
// {
//     public class TrainController : PersistentSingleton<TrainController>, ISaveable
//     {
//         private readonly TrainServiceManager _trainServiceManager = new TrainServiceManager();
//         private readonly TrainConsistRepository _trainRepository = new TrainConsistRepository();
//         private TrainSaveManager _trainSaveManager;
//
//         public event Action OnTrainListUpdated
//         {
//             add => _trainRepository.OnTrainListUpdated += value;
//             remove => _trainRepository.OnTrainListUpdated -= value;
//         }
//         
//         /// <summary>
//         /// Dispatches a train for service using the station's data.
//         /// </summary>
//         /// <param name="trainConsist">The train consist to dispatch.</param>
//         /// <param name="station">The station asset, which contains route and load rate info.</param>
//         /// <param name="masterType">Specifies whether this station handles loading (departing) or unloading (arriving).</param>
//         /// <param name="capacity">The capacity (load amount) to be processed.</param>
//         /// <returns>True if dispatch was successful.</returns>
//         public bool DispatchTrain(TrainConsistModel trainConsist, Station station, StationMasterType masterType, int capacity)
//         {
//             if (_trainServiceManager.IsTrainInService(trainConsist))
//             {
//                 Console.WriteLine($"Train {trainConsist.name} is already in service!");
//                 return false;
//             }
//             
//             if (_trainServiceManager.StartService(station, trainConsist, masterType, capacity))
//             {
//                 _trainRepository.DispatchTrain(trainConsist);
//                 return true;
//             }
//             return false;
//         }
//         
//         /// <summary>
//         /// Recalls a train from active service.
//         /// </summary>
//         /// <param name="trainConsist">The train consist to recall.</param>
//         /// <returns>True if recall was successful.</returns>
//         public bool RecallTrain(TrainConsistModel trainConsist)
//         {
//             if (_trainServiceManager.FinishService(trainConsist))
//             {
//                 _trainRepository.RecallTrain(trainConsist);
//                 return true;
//             }
//             return false;
//         }
//         
//         public void AddTrainConsist(TrainConsistModel trainConsist) 
//             => _trainRepository.AddTrain(trainConsist);
//
//         public void RemoveTrainConsist(TrainConsistModel trainConsist) 
//             => _trainRepository.RemoveTrain(trainConsist);
//         
//         public List<TrainConsistModel> GetAllTrainConsists() 
//             => _trainRepository.GetAllTrains();
//
//         public List<TrainConsistModel> GetAllTrainConsists(ServiceStatus serviceStatus) 
//             => _trainRepository.GetAllTrains(serviceStatus);
//         
//         /// <summary>
//         /// Update all active services. Call this once per frame.
//         /// </summary>
//         /// <param name="deltaTime">Time elapsed since the last update.</param>
//         public void UpdateServices(float deltaTime)
//         {
//             _trainServiceManager.UpdateAllServices(deltaTime);
//         }
//
//         public void PopulateSaveData(SaveData sd) 
//             => TrainSaveManager.PopulateSaveData(_trainRepository.GetAllTrains(), sd);
//
//         public void LoadFromSaveData(SaveData sd)
//         {
//             _trainRepository.Clear();
//             _trainRepository.AddTrains(TrainSaveManager.LoadFromSaveData(sd));
//         }
//         
//         public void Reset()
//         {
//             foreach (var train in _trainRepository.GetAllTrains())
//             {
//                 train.Reset();
//             }
//             _trainRepository.Clear();
//         }
//     }
// }