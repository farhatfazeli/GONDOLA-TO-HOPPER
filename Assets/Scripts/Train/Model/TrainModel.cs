using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;

namespace Train.Model
{
    public class TrainModel
    {
        public Locomotive Locomotive { get; private set; }
        public List<Wagon> Wagons { get; private set; }
        
        private TrainEngine _trainEngine;
        
        public TrainModel(Locomotive locomotive)
        {
            Locomotive = locomotive;
            Wagons = new List<Wagon>();
            _trainEngine = new TrainEngine(locomotive, GetTotalMass());
        }
        
        public void AddWagon(Wagon wagon)
        {
            Wagons.Add(wagon);
        }

        private float GetTotalMass()
        {
            return Locomotive.mass + Wagons.Sum(wagon => wagon.mass);
        }
    }
}