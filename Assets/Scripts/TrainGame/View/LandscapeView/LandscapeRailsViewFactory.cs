using System;
using UnityEngine;

namespace TrainGame.View.LandscapeView
{
    public class LandscapeRailsViewFactory : MonoBehaviour
    {
        [Header("Build settings")]
        [SerializeField] private float autoBuildPeriod = 1f;
        [SerializeField] private float buildDistance = 0.1f;
        [SerializeField] private GameObject railPrefab;
        [SerializeField] private Transform cameraTarget;
        
        private Vector3 _lastRailPosition;
        private float _timeSinceLastBuild;

        private void Start()
        {
            _lastRailPosition = transform.position - Vector3.right * buildDistance;
        }

        private void Update()
        {
            AutoBuild();
        }

        private void AutoBuild()
        {
            _timeSinceLastBuild += Time.deltaTime;
            
            if(autoBuildPeriod <= 0) return;
            
            if (_timeSinceLastBuild < 1 / autoBuildPeriod) return;
            
            BuildRail();

            _timeSinceLastBuild = 0;
        }
        
        public void OnTrackClicked()
        {
            BuildRail();
        }
        
        private void BuildRail()
        {
            Instantiate(railPrefab, _lastRailPosition, Quaternion.identity, transform);
            _lastRailPosition += Vector3.right * buildDistance;
            cameraTarget.position = _lastRailPosition;
        }
    }
}