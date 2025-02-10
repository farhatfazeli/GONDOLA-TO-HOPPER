using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public float passengerKm;
    public float tonneKm;
    
    public List<TrainObject> trainObjects = new List<TrainObject>();
}

public interface ISaveable
{
    void PopulateSaveData(SaveData sd);
    void LoadFromSaveData(SaveData sd);
}