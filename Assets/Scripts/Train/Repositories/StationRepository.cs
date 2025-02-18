using System;
using System.Collections.Generic;
using System.Linq;
using Train.Model;
using Train.Model.Station;

namespace Train.Repositories
{
    public class StationRepository
    {
        private readonly List<StationModel> _stations = new List<StationModel>();
        
        public void AddStations(IEnumerable<StationModel> stations)
        {
            _stations.AddRange(stations);
        }
        
        public List<StationModel> GetAllStations()
        {
            return new List<StationModel>(_stations);
        }

        private StationModel GetStationByID(string uuid)
        {
            return _stations.FirstOrDefault(s => s.uuid == uuid);
        }

        public void Clear()
        {
            _stations.Clear();
        }

        private static StationRepository instance;
        public static StationRepository I => instance ??= new StationRepository();
        private StationRepository()
        {
        }
    }
}
