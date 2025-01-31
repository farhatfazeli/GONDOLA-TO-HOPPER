using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewStation", menuName = "TrainGame/Station")]
    public class Station : ScriptableObject
    {
        public string stationName;
        
        public int resourceCost;
        public Progress buildProgress;
        
        public Route route;
    }
}