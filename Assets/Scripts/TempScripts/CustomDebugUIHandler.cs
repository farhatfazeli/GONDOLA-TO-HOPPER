using ScriptableObjects;
using TMPro;
using Train;
using Train.View;
using UnityEngine;

namespace TempScripts
{
    public class CustomDebugUIHandler : MonoBehaviour
    {
        public TextMeshProUGUI speedText;

        private TrainView _trainView;

        public void Start()
        {
            _trainView = FindFirstObjectByType<TrainView>();
            
        }

        private void Update()
        {
            DebugTrainEngineValues();
        }

        private void DebugTrainEngineValues()
        {
            var (speed, acceleration, tractionForce, pulledMass) = _trainView.GetPhysicsValues();
            speedText.text = "Speed: " + speed + " m/s" + "\n" +
                             "Acceleration: " + acceleration + " m/s²" + "\n" +
                             "Traction Force: " + tractionForce + " N" + "\n" +
                             "Pulled Mass: " + pulledMass + " kg";
        }

    }
}