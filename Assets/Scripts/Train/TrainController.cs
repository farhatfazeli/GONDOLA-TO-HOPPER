using System.Collections.Generic;
using ScriptableObjects;
using Trains.Model;
using Trains.View;
using UnityEngine;

namespace Train
{
    public class TrainController : PersistentSingleton<TrainController>, ISaveable
    {
        public List<TrainObject> trains = new List<TrainObject>();

        public void Reset()
        {
            foreach (var train in trains)
            {
                train.Reset();
            }
            trains.Clear();
        }

        public void PopulateSaveData(SaveData sd)
        {
            foreach (var train in trains)
            {
                SaveData.TrainData td = new SaveData.TrainData
                {
                    uuid = train.uuid,
                    name = train.name,
                    maxSpeed = train.maxSpeed,
                    tractionCoefficient = train.tractionCoefficient,
                    brakingCoefficient = train.brakingCoefficient,
                    mass = train.mass
                };
                sd.trainData.Add(td);
            }
        }

        public void LoadFromSaveData(SaveData sd)
        {
            trains.Clear();

            foreach (var trainData in sd.trainData)
            {
                TrainObject train = new TrainObject(trainData.uuid, trainData.name,trainData.maxSpeed, trainData.tractionCoefficient, trainData.brakingCoefficient, trainData.mass);
                trains.Add(train);
            }
        }
        // private readonly List<TrainModel> _trainModels = new();
        //
        // public TrainModel trainModel;
        //
        // public Locomotive testLocomotive;
        //
        // public float lolfloat;
        //
        // private void Start()
        // {
        //     CreateTrainModel(testLocomotive);
        // }
        //
        // private void Update()
        // {
        //     foreach (var model in _trainModels)
        //         model.Update(Time.deltaTime);
        // }
        //
        // private TrainModel CreateTrainModel(Locomotive locomotive)
        // {
        //     trainModel = new TrainModel(locomotive);
        //     _trainModels.Add(trainModel);
        //     var trainView = FindFirstObjectByType<TrainView>();
        //     trainView.trainModel = trainModel;
        //     return trainModel;
        // }
        //
        // public void DispatchTrain()
        // {
        //     trainModel.StartEngine();
        //     Debug.Log("Train dispfatched");
        // }
        //
        // public void ShowView()
        // {
        //     Debug.Log("Train view shown");
        // }
        //
        // public void DestroyView()
        // {
        //     Debug.Log("Train view destroyed");
        // }
        
        
    }
}