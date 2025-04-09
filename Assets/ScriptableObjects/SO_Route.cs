using System;
using System.Collections.Generic;
using TrainGame.Model;
using TrainGame.Model.RollingStock;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewRoute", menuName = "TrainGame/Route")]
    public class SO_Route : ScriptableObject
    {
        public string uuid;
        
        [Header ("Route settings")]
        public SO_Station departureStation;
        public SO_Station arrivalStation;
        public float distance;
        
        [Header("Build settings")]
        public bool isBuiltAtStart;
        public LoadType buildResourceType;
        public int buildResourceCost;
        public int maxBuildPoints;
        public int baseBuildAutoRate;
        public int baseBuildManualRate;

        [Header("Scene settings")]
        public SceneAsset sceneAsset;
        
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(uuid))
            {
                uuid = Guid.NewGuid().ToString();
            }
        }
    }
}