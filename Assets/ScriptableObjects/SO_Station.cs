using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewStation", menuName = "TrainGame/Station")]
    public class Station : ScriptableObject
    {
        public string stationName;
        
        public int buildCost;
        
        public List<Route> availableRoutes;

        public float basePassengerLoadRate;
        public float baseFreightLoadRate;
    }
}