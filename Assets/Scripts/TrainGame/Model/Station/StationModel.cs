using ScriptableObjects;

namespace TrainGame.Model.Station
{
    public class StationModel
    {
        public string uuid;
        public string name;
        public bool isBuilt;
        public float passengerLoadRate;
        public float freightLoadRate;

        public StationModel(SO_Station station) : this(station.uuid, station.name, false,
            station.basePassengerLoadRate, station.baseFreightLoadRate)
        {
        }

        public StationModel(string uuid, string name, bool isBuilt, float passengerLoadRate, float freightLoadRate)
        {
            this.uuid = uuid;
            this.name = name;
            this.isBuilt = isBuilt;
            this.passengerLoadRate = passengerLoadRate;
            this.freightLoadRate = freightLoadRate;
        }
    }
}