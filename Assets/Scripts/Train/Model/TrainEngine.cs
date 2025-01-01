using ScriptableObjects;
using UnityEngine;

namespace Train.Model
{
    public class TrainEngine
    {
        public bool Running { get; private set; } = false;

        // Train state variables
        public float Position { get; private set; } = 0f;
        public float Speed { get; private set; } = 0f;
        public float Acceleration { get; private set; } = 0f;
        public float TractionForce { get; private set; } = 0f;
        
        // Train parameters
        private readonly float _maxSpeed;         // Maximum speed (m/s)
        private readonly float _tractionCoefficient; // Traction coefficient
        private readonly float _brakingCoefficient;  // Braking coefficient
        private readonly float _pulledMass; // Mass of the train

        // Constructor
        public TrainEngine(Locomotive locomotive, float pulledMass)
        {
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
                if (Speed > 0)
                {
                    HandlePhysics(deltaTime, _brakingCoefficient);
                }
            }
        }

        private void HandlePhysics(float deltaTime, float coefficient)
        {
            TractionForce = FunctionLibrary.TractionCalculator(Speed, _maxSpeed, coefficient);
            Acceleration = TractionForce / _pulledMass;
            Speed += Acceleration * deltaTime;
            Speed = Mathf.Clamp(Speed, 0, _maxSpeed);
            Position += Speed * deltaTime;
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