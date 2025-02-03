using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLocomotive", menuName = "TrainGame/Locomotive")]
    public class Locomotive : RollingStock
    {
        public override RollingStockType Type => RollingStockType.Locomotive;
        
        //physics parameters
        public float maxSpeed;
        public float tractionCoefficient;
        public float brakingCoefficient;
        
        [Header ("Rolling stock parameters")]
        [SerializeField] private int mass;  // Private field for inspector
        [SerializeField] private Sprite depotSprite;
        [SerializeField] private int availableAmount;
        [SerializeField] private AchievementState achievementState;
        [SerializeField] private int unlockCost;

        // Exposed properties (read-only or read-write)
        public override int Mass => mass;  // Read-only property
        public override Sprite DepotSprite => depotSprite;  // Read-only property
        public override int AvailableAmount { get => availableAmount; set => availableAmount = value; }  // Read-write property
        public override AchievementState AchievementState { get => achievementState; set => achievementState = value; }
        public override int UnlockCost => unlockCost;
        
    }
}