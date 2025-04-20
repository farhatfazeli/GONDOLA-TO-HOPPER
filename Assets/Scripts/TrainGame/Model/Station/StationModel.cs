using System;
using System.Collections.Generic;
using Core.Utility;
using ScriptableObjects;

namespace TrainGame.Model.Station
{
    public class StationModel : IIdentifiable, IModelObservable
    {
        public string uuid { get; }
        public string name { get; }

        public float passengerLoadRate;
        public float freightLoadRate;

        public readonly StationBuilder stationBuilder;

        public float baseLoadAutoRate;
        public float baseLoadManualRate;

        public StationModel(SO_Station soStation)
        {
            uuid = soStation.uuid;
            name = soStation.name;

            stationBuilder = new StationBuilder(soStation, this);
            
            baseLoadAutoRate = soStation.baseLoadAutoRate;
            baseLoadManualRate = soStation.baseLoadManualRate;
        }

        public event Action OnModelChanged;
    }
}