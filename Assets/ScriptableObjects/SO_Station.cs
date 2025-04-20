using System;
using TrainGame.Model.RollingStock;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewStation", menuName = "TrainGame/Station")]
    public class SO_Station : ScriptableObject
    {
        public string uuid;

        [Header("Load settings")]
        public float baseLoadAutoRate;
        public float baseLoadManualRate;
        
        [Header("Build settings")] 
        public bool isBuiltAtStart;
        public LoadType buildResourceType;
        public int buildResourceCost;
        public int maxBuildPoints;
        public int baseBuildAutoRate;
        public int baseBuildManualRate;
        
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