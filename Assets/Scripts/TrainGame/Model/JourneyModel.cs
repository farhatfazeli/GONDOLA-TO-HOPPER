using System;
using UnityEngine;

namespace TrainGame.Model
{
    public class JourneyModel : IProgressTarget
    {
        public float Current {
            get => _currentDistance;
            private set
            {
                _currentDistance = Mathf.Clamp(value, 0f, Max);
                if (_currentDistance >= _brakingDistance)
                    OnBrakingDistanceReached?.Invoke();
            }
        }

        public float Max => _endDistance;

        
        public event Action OnBrakingDistanceReached;
        
        private readonly float _brakingDistance;
        
        private float _currentDistance;

        private float _endDistance;
        public JourneyModel(float endDistance, float brakingDistance)
        {
            if (endDistance <= 0)
                throw new ArgumentException("End distance must be greater than zero.", nameof(endDistance));

            _endDistance = endDistance;
            _brakingDistance = brakingDistance;
            _currentDistance = 0f;
        }
        
        public void UpdateProgress(float amount)
        {
            CoverDistance(amount);
        }

        private void CoverDistance(float distance)
        {
            if (distance < 0)
                throw new ArgumentException("Distance must be greater than zero.", nameof(distance));
            Current += distance;
        }
    }
}