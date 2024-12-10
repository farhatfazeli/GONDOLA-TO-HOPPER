using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewRoute", menuName = "TrainGame/Route")]
    public class Route : ScriptableObject
    {
        public string routeName;
        public Station startStation;
        public Station endStation;
        public float distance;
        public float travelTime;
    
        public List<Landscape> landscapes;
    }
}