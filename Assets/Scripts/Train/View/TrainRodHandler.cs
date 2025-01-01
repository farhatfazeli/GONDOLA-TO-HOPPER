using UnityEngine;

namespace Train.View
{
    public class TrainRodHandler : MonoBehaviour
    {
        public TrainWheelHandler attachedWheelHandler;

        public Transform lookAtPoint;

        private Vector2 _offset;
        void Start()
        {
            _offset = transform.position - attachedWheelHandler.transform.position;
        }

        void Update()
        {
            float angle = attachedWheelHandler.transform.eulerAngles.z * Mathf.Deg2Rad; // Convert angle to radians
            Vector3 drivingPoint = attachedWheelHandler.transform.position + 
                                   new Vector3(_offset.magnitude * Mathf.Cos(angle), _offset.magnitude * Mathf.Sin(angle), 0);
            transform.position = drivingPoint;
            transform.LookAt(lookAtPoint);
            //rotate rod to match the angle of the wheel
            transform.Rotate(0, 90, 0);
        }
    }
}
