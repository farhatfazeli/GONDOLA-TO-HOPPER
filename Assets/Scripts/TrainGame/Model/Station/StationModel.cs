using System;
using System.Collections.Generic;
using Core.Utility;
using ScriptableObjects;
using UnityEngine;

namespace TrainGame.Model.Station
{
    public class StationModel : IIdentifiable, IModelObservable
    {
        public string uuid { get; }
        public string name { get; }

        public float passengerLoadRate;
        public float freightLoadRate;

        public readonly StationBuilder stationBuilder;

        public float baseLoadAutoRate { get; }
        public float baseLoadManualRate { get; }

        public StationModel(SO_Station soStation)
        {
            uuid = soStation.uuid;
            name = soStation.name;
            
            stationBuilder = new StationBuilder(this,soStation);
            
            baseLoadAutoRate = soStation.baseLoadAutoRate;
            baseLoadManualRate = soStation.baseLoadManualRate;
        }

        public event Action OnModelChanged;
    }
}