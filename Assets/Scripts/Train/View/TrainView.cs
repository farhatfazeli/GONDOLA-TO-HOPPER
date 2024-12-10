using ScriptableObjects;
using Train.Model;
using UnityEngine;

namespace Train.View
{
    public class TrainView : MonoBehaviour
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