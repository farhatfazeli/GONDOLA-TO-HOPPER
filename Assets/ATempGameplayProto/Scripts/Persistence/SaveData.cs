using System;

[Serializable]
public class SaveData
{
    public float passengerKm;
    public float tonneKm;
}

public interface ISaveable
{
    void PopulateSaveData(SaveData sd);
    void LoadFromSaveData(SaveData sd);
}