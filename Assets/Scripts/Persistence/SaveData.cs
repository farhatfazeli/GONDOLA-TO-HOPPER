using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public float passengerKm;
    public float tonneKm;
    
    public List<TrainData> trainData = new List<TrainData>();
    
    [Serializable]
    public struct TrainData
    {
        public int uuid;
        public string name;
        public float maxSpeed;
        public float tractionCoefficient;
        public float brakingCoefficient;
        public float mass;
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData sd);
    void LoadFromSaveData(SaveData sd);
}