using System;
using TMPro;
using UnityEngine;

namespace ATrainGamePrototype.Scripts
{
    public class DebugValueHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _positionText;
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private TextMeshProUGUI _accelerationText;

        private void Update() {
            _positionText.text = "Position: " + TrainController.I.trainEngine.Position.ToString("F2");
            _speedText.text = "Speed: " + TrainController.I.trainEngine.Speed.ToString("F2");
            _accelerationText.text = "Acceleration: " + TrainController.I.trainEngine.Acceleration.ToString("F2");;
        }
    }
}