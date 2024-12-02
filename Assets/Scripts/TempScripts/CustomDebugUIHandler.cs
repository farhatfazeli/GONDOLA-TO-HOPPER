using TMPro;
using Train;
using UnityEngine;
using UnityEngine.UI;

namespace TempScripts
{
    public class CustomDebugUIHandler : MonoBehaviour
    {
        public TextMeshProUGUI speedText;

        private TrainController _trainController;

        public
            // Start is called once before the first execution of Update after the MonoBehaviour is created
            void Start()
        {
            _trainController = FindFirstObjectByType<TrainController>();
        }

        // Update is called once per frame
        private void Update()
        {
            DebugTrainEngineValues();
        }

        private void DebugTrainEngineValues()
        {
            var (speed, acceleration, tractionForce, pulledMass) = _trainController.GetPhysicsValues();
            speedText.text = "Speed: " + speed + " m/s" + "\n" +
                             "Acceleration: " + acceleration + " m/s²" + "\n" +
                             "Traction Force: " + tractionForce + " N" + "\n" +
                             "Pulled Mass: " + pulledMass + " kg";
        }
    }
}