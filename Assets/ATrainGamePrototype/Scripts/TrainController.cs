using System.Linq;
using Core.Utility;
using ScriptableObjects;
using TrainGame.Model.Route;
using TrainGame.Model.TrainConsist;
using UnityEngine;

namespace ATrainGamePrototype.Scripts
{
    public class TrainController : PersistentSingleton<TrainController>
    {
        [Header("Train data")]
        [SerializeField] private SO_Locomotive _locomotive;
        [SerializeField] private SO_Wagon[] _wagons;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource _whistleAudio;
        
        public TrainEngine trainEngine;
        
        private void Awake() {
            Initialize();
        }

        private void Update() {
            trainEngine.Update(Time.deltaTime);
            transform.position = new Vector3(trainEngine.Position, transform.position.y, 0f);
        }

        private void Initialize() {
            trainEngine = new TrainEngine(_locomotive.maxSpeed, _locomotive.tractionCoefficient, _locomotive.brakingCoefficient, _locomotive.mass);
            trainEngine.UpdateLoad(_wagons.Sum(x => x.mass));
        }

        public void SetAcceleration(float normalisedAcceleration) {
            trainEngine.SetAcceleration(normalisedAcceleration);
        }

        public void BlowWhistle(float normalisedDisplacement) {
            if (!_whistleAudio.isPlaying && normalisedDisplacement > 0.6f)
                _whistleAudio.Play();
            else if (_whistleAudio.isPlaying &&  normalisedDisplacement < 0.6f)
                _whistleAudio.Stop();
        }
    }
}