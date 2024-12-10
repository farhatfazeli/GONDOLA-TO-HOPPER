using ScriptableObjects;
using UnityEngine;

namespace Train.View
{
    public class TrainWheelHandler : MonoBehaviour
    {
        public Wheel wheel;

        public void RotateWheel(float speed)
        {
            transform.Rotate(Vector3.back, wheel.GetAngularVelocity(speed) * 360 * Time.deltaTime);
        }
    }
}
