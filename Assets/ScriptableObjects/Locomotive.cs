using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLocomotive", menuName = "TrainGame/Locomotive")]
    public class Locomotive : ScriptableObject
    {
        public float maxSpeed;
        public float tractionCoefficient;
        public float brakingCoefficient;
        public float mass;
    }
}