using System;
using System.Collections.Generic;
using TrainGame.Model.RollingStock;

namespace Persistence
{
    [Serializable]
    public class SaveData
    {
        public float passengerKm;
        public float tonneKm;

        public long savedGameTime;
        public long savedRealTime;
    
        public List<TrainConsistSaveData> trainConsistSD = new List<TrainConsistSaveData>();
        
        public List<StationSaveData> stationSD = new List<StationSaveData>();
        
        public List<RollingStockSaveData> rollingStockSD = new List<RollingStockSaveData>();
    }
    
    [Serializable]
    public struct StationSaveData
    {
        public string uuid;
        public string name;
        public bool isBuilt;
        public float passengerLoadRate;
        public float freightLoadRate;
    }
    
    [Serializable]
    public struct RollingStockSaveData
    {
        public string uuid;
        public int availableAmount;
        public int fleetAmount;
    }

    [Serializable]
    public struct TrainConsistSaveData
    {
        public int uuid;
        public string name;
        public List<string> rollingStockUuids;
    }

    public interface ISaveable
    {
        void PopulateSaveData(SaveData sd);
        void LoadFromSaveData(SaveData sd);
    }
}