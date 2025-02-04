using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWagon", menuName = "TrainGame/Wagon")]
    public class Wagon : RollingStock
    {
        [Header ("Wagon parameters")]
        public CargoType cargoType;
        public override RollingStockType Type => RollingStockType.Wagon;
        
        // [Header ("Rolling stock parameters")]
        // [SerializeField] private int mass;  // Private field for inspector
        // [SerializeField] private Sprite depotSprite;
        // [SerializeField] private int availableAmount;
        // [SerializeField] private AchievementState achievementState;
        // [SerializeField] private int unlockCost;
        // // [SerializeField] private List<RollingStock> prerequisiteRollingStock;
        //
        // // Exposed properties (read-only or read-write)
        // public override int Mass => mass;  // Read-only property
        // public override Sprite DepotSprite => depotSprite;  // Read-only property
        // public override int AvailableAmount { get => availableAmount; set => availableAmount = value; }  // Read-write property
        // public override AchievementState AchievementState { get => achievementState; set => achievementState = value; }
        // public override int UnlockCost => unlockCost;
        // // public override List<RollingStock> PrerequisiteRollingStock => prerequisiteRollingStock;
    }
    
    public enum CargoType
    {
        Passengers,
        Freight
    }
}