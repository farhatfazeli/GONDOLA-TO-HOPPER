using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLocomotive", menuName = "TrainGame/Locomotive")]
    public class Locomotive : RollingStock
    {
        //physics parameters
        public float maxSpeed;
        public float tractionCoefficient;
        public float brakingCoefficient;
        
        [Header ("Rolling stock parameters")]
        [SerializeField] private int mass;  // Private field for inspector
        [SerializeField] private Sprite depotSprite;
        [SerializeField] private int availableAmount;

        // Exposed properties (read-only or read-write)
        public override int Mass => mass;  // Read-only property
        public override Sprite DepotSprite => depotSprite;  // Read-only property
        public override int AvailableAmount { get => availableAmount; set => availableAmount = value; }  // Read-write property
        
    }
}