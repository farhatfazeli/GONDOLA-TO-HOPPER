using System.Collections.Generic;
using ScriptableObjects;
using TrainGame.Repositories;

namespace TrainGame.Model.Station
{
    public class StationModel : IIdentifiable
    {
        public string uuid { get; }
        public string name { get; }

        public float passengerLoadRate;
        public float freightLoadRate;

        public StationBuilder stationBuilder;
        public List<StationMaster> stationMaster;


        public StationModel(SO_Station station)
        {
            uuid = station.uuid;
            name = station.name;

            stationBuilder = new StationBuilder(station);
            stationMaster = new List<StationMaster>();
        }
    }
}