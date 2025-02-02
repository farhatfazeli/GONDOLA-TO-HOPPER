using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWagon", menuName = "TrainGame/Wagon")]
    public class Wagon : RollingStock
    {
        public CargoType cargoType;
        
        [Header ("Rolling stock parameters")]
        [SerializeField] private int mass;  // Private field for inspector
        [SerializeField] private Sprite depotSprite;
        [SerializeField] private int availableAmount;

        // Exposed properties (read-only or read-write)
        public override int Mass => mass;  // Read-only property
        public override Sprite DepotSprite => depotSprite;  // Read-only property
        public override int AvailableAmount { get => availableAmount; set => availableAmount = value; }  // Read-write property
    }
    
    public enum CargoType
    {
        Passengers,
        Freight
    }
}