using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Train
{
    public class TrainWheelHandler : MonoBehaviour
    {
        public Wheel wheel;

        public void RotateWheel(float speed)
        {
            transform.Rotate(Vector3.back, wheel.GetAngularVelocity(speed) * 360 * Time.deltaTime);
            Debug.Log(transform.rotation.eulerAngles);
        }
    }
}
