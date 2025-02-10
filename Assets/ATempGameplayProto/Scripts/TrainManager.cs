using System.Collections.Generic;
using UnityEngine;

public class TrainManager : PersistentSingleton<TrainManager>, ISaveable
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
}
