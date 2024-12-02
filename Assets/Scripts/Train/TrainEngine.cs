using ScriptableObjects;
using UnityEngine;

namespace Train
{
    public class TrainEngine
    {
        public bool Running { get; private set; } = false;

        // Train state variables
        public float CurrentSpeed { get; private set; } = 0f;
        public float CurrentAcceleration { get; private set; } = 0f;
        public float CurrentTractionForce { get; private set; } = 0f;

        // Train transform
        private readonly Transform _trainRoot; // Train root transform
        
        // Train parameters
        private readonly float _maxSpeed;         // Maximum speed (m/s)
        private readonly float _tractionCoefficient; // Traction coefficient
        private readonly float _brakingCoefficient;  // Braking coefficient
        private readonly float _pulledMass; // Mass of the train

        // Constructor
        public TrainEngine(Transform trainRoot, Locomotive locomotive, float pulledMass)
        {
            _trainRoot = trainRoot;
            _maxSpeed = locomotive.maxSpeed;
            _tractionCoefficient = locomotive.tractionCoefficient;
            _brakingCoefficient = locomotive.brakingCoefficient;
            _pulledMass = pulledMass;
            SanityCheck();
        }


        // Start accelerating the train
        public void Start()
        {
            Running = true;
        }

        // Stop the train (begin deceleration)
        public void Stop()
        {
            Running = false;
        }

        // Update the train's physics and return the delta position
        public void Update(float deltaTime)
        {
            if (Running)
            {
                HandlePhysics(deltaTime, _tractionCoefficient);
            }
            else
            {
                if (CurrentSpeed > 0)
                {
                    HandlePhysics(deltaTime, _brakingCoefficient);
                }
            }
        }

        private void HandlePhysics(float deltaTime, float coefficient)
        {
            CurrentTractionForce = FunctionLibrary.TractionCalculator(CurrentSpeed, _maxSpeed, coefficient);
            CurrentAcceleration = CurrentTractionForce / _pulledMass;
            CurrentSpeed += CurrentAcceleration * deltaTime;
            CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0, _maxSpeed);
            _trainRoot.position += _trainRoot.right * (CurrentSpeed * deltaTime);
        }

        // Get current speed
        public float GetCurrentSpeed()
        {
            return CurrentSpeed;
        }
    
    
        private void SanityCheck()
        {
            if (_maxSpeed <= 0)
                throw new System.ArgumentException("maxSpeed must be greater than zero");
            if (_tractionCoefficient <= 0)
                throw new System.ArgumentException("tractionCoefficient must be greater than zero");
            if (_brakingCoefficient >= 0)
                throw new System.ArgumentException("brakingCoefficient must be smaller than zero");
        }
    }
}