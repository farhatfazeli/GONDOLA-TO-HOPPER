using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewStation", menuName = "TrainGame/Station")]
    public class SO_Station : ScriptableObject
    {
        public string uuid;
        
        public string stationName;
        
        public int buildCost;

        public float basePassengerLoadRate;
        public float baseFreightLoadRate;
        
        // OnValidate is called in the editor whenever the asset is modified.
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(uuid))
            {
                uuid = Guid.NewGuid().ToString();
            }
        }
    }
}