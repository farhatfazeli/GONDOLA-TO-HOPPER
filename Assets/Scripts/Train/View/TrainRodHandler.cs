using UnityEngine;

namespace Train.View
{
    public class TrainRodHandler : MonoBehaviour
    {
        public TrainWheelHandler attachedWheelHandler;

        private Vector2 _offset;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _offset = transform.position - attachedWheelHandler.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            float angle = attachedWheelHandler.transform.eulerAngles.z * Mathf.Deg2Rad; // Convert angle to radians
            Vector3 drivingPoint = attachedWheelHandler.transform.position + 
                                   new Vector3(_offset.magnitude * Mathf.Cos(angle), _offset.magnitude * Mathf.Sin(angle), 0);
            transform.position = drivingPoint;
        }
    }
}
