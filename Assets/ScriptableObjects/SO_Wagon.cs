using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWagon", menuName = "TrainGame/Wagon")]
    public class Wagon : ScriptableObject
    {
        public CargoType cargoType;
        public float mass;
    }
    
    public enum CargoType
    {
        Passengers,
        Freight
    }
}