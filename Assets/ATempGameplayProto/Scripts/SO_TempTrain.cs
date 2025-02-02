using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TempTrain", menuName = "TrainGame/TempTrain")]
    public class TempTrain : ScriptableObject
    {
        public string Name => name;
        public float maxSpeed;
        public float tractionCoefficient;
        public float brakingCoefficient;
        public float mass;
    }
}