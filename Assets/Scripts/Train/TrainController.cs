using System;
using ScriptableObjects;
using UnityEngine;

namespace Train
{
    public class TrainController : MonoBehaviour
    {
        public Locomotive locomotive;
        public float pulledMass;

        private TrainEngine _trainEngine;
        private TrainWheelController _wheelController;

        private void Awake()
        {
            _trainEngine = new TrainEngine(this.transform, locomotive, pulledMass);
            _wheelController = GetComponentInChildren<TrainWheelController>();
        }

        private void Update()
        {
            _trainEngine.Update(Time.deltaTime);
            _wheelController.RotateWheels(_trainEngine.CurrentSpeed);
            // turn train with left right arrows
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.up, -1f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up, 1f);
            }
        }

        public void DispatchTrain()
        {
            _trainEngine.Start();
            Debug.Log("Train dispatched");
        }
        
        public (float, float, float, float) GetPhysicsValues()
        {
            return (_trainEngine.CurrentSpeed, _trainEngine.CurrentAcceleration, _trainEngine.CurrentTractionForce, pulledMass);
        }
    }
}