using UnityEngine;

namespace TrainGame.Model
{
    public class TrainEngine
    {
        // Train state variables
        public float Speed { get; private set; } = 0f;
        public float Acceleration { get; private set; } = 0f;
        private float TotalMass => _locomotiveMass + _load; // Total mass of the train
        
        private float _load; // Load of the train
        private float _targetAcceleration; // Target acceleration
        
        // Train parameters
        private readonly float _maxSpeed;         // Maximum speed (m/s)
        private readonly float _tractionCoefficient; // Traction coefficient
        private readonly float _brakingCoefficient;  // Braking coefficient
        private readonly float _locomotiveMass; // Dead mass of the train
        
        
        public TrainEngine(float maxSpeed, float tractionCoefficient, float brakingCoefficient, float locomotiveMass)
        {
            _maxSpeed = maxSpeed;
            _tractionCoefficient = tractionCoefficient;
            _brakingCoefficient = brakingCoefficient;
            _locomotiveMass = Mathf.Max(locomotiveMass, 1f);
            SanityCheck();
        }
        
        // Update the train's physics and return the delta position
        public void Update(float deltaTime)
        {
            if (Speed <= 0 && _targetAcceleration <= 0) return;
            HandlePhysics(deltaTime);
        }

        private void HandlePhysics(float deltaTime)
        {
            float coefficient = _targetAcceleration > 0 ? _tractionCoefficient : _brakingCoefficient;
            float tractionForce = FunctionLibrary.TractionCalculator(Speed, _maxSpeed, coefficient);
            Acceleration = tractionForce / TotalMass;
            
            Speed += Acceleration * deltaTime;
            Speed = Mathf.Clamp(Speed, 0, _maxSpeed);
        }

        public void SetAccelerationMode(bool isAccelerating)
        {
            _targetAcceleration = isAccelerating ? 1f : -1f;
        }
        
        public void UpdateLoad(float newLoad)
        {
            if(Speed > 0)
                throw new System.InvalidOperationException("Cannot update load while train is moving");
            if (newLoad < 0)
                throw new System.ArgumentException("Load must be greater than zero");
            _load = newLoad;
        }
        
        private void SanityCheck()
        {
            if (_locomotiveMass <= 0)
                throw new System.ArgumentException("locomotiveMass must be greater than zero");
            if (_maxSpeed <= 0)
                throw new System.ArgumentException("maxSpeed must be greater than zero");
            if (_tractionCoefficient <= 0)
                throw new System.ArgumentException("tractionCoefficient must be greater than zero");
            if (_brakingCoefficient >= 0)
                throw new System.ArgumentException("brakingCoefficient must be smaller than zero");
        }

        public float CalculateBrakingDistance()
        {
            float brakingForce = FunctionLibrary.TractionCalculator(_maxSpeed, _maxSpeed, _brakingCoefficient);
            float brakingDistance = (_maxSpeed * _maxSpeed) / (2 * brakingForce);
            
            if (brakingDistance < 0)
                throw new System.InvalidOperationException("Braking distance cannot be negative");

            return brakingDistance;
        }
    }
}