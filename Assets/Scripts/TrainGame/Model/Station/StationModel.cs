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

        public StationModel(SO_Station station)
        {
            uuid = station.uuid;
            name = station.name;

            stationBuilder = new StationBuilder(station);
            
            baseLoadAutoRate = station.baseLoadAutoRate;
            baseLoadManualRate = station.baseLoadManualRate;
        }

        public event Action OnModelChanged;
    }
}