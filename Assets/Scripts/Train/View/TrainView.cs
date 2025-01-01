using ScriptableObjects;
using Train.Model;
using UnityEngine;

namespace Train.View
{
    public class TrainView : MonoBehaviour
    {
        public TrainModel trainModel;
        private TrainWheelController _wheelController;

        private void Awake()
        {
            _wheelController = GetComponentInChildren<TrainWheelController>();
        }

        private void Update()
        {
            transform.position = trainModel.TransmitCurrentPosition() * Vector3.right;
            _wheelController.RotateWheels(trainModel.TransmitCurrentSpeed());
        }
        
        public (float, float, float, float) GetPhysicsValues()
        {
            return trainModel.TransmitTrainState();
        }
    }
}