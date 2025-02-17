using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewRoute", menuName = "TrainGame/Route")]
    public class Route : ScriptableObject
    {
        public string routeName;
        public Station departureStation;
        public Station arrivalStation;
        public float distance;
        
        public int resourceCost;
        public ProgressTracker buildProgressTracker;
    
        public List<Landscape> landscapes;
    }
}