using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWagon", menuName = "TrainGame/Wagon")]
    public class Wagon : RollingStock
    {
        public override RollingStockType Type => RollingStockType.Wagon;
        
        [Header ("Wagon parameters")]
        public CargoType cargoType;
    }
    
    public enum CargoType
    {
        Passengers,
        Freight
    }
}