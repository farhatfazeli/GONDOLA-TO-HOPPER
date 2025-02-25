using System.Collections.Generic;
using TrainGame.Model;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewRoute", menuName = "TrainGame/Route")]
    public class Route : ScriptableObject
    {
        public string routeName;
        public SO_Station departureSoStation;
        public SO_Station arrivalSoStation;
        public float distance;
        
        public int resourceCost;
        public ProgressTracker buildProgressTracker;
    
        public List<Landscape> landscapes;
    }
}