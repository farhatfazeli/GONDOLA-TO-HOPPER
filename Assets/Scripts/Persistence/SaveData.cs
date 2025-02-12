using System;
using System.Collections.Generic;

namespace Persistence
{
    [Serializable]
    public class SaveData
    {
        public float passengerKm;
        public float tonneKm;
    
        public List<TrainConsistSaveData> trainConsistSD = new List<TrainConsistSaveData>();
    
    

    }

    [Serializable]
    public struct TrainConsistSaveData
    {
        public int uuid;
        public string name;
        public List<RollingStock> rollingStock;
    }

    public interface ISaveable
    {
        void PopulateSaveData(SaveData sd);
        void LoadFromSaveData(SaveData sd);
    }
}