using System.Collections.Generic;
using UnityEngine;

namespace Train.View
{
    public class TrainWheelController : MonoBehaviour
    {
        public List<TrainWheelHandler> wheelHandlers;

        public void RotateWheels(float speed)
        {
            foreach (var wheelHandler in wheelHandlers) wheelHandler.RotateWheel(speed);
        }
    }
}