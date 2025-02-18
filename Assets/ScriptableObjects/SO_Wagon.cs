using Train.Model.RollingStock;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWagon", menuName = "TrainGame/Wagon")]
    public class SO_Wagon : SO_RollingStock
    {
        public override RollingStockType Type => RollingStockType.Wagon;

        [Header("Wagon parameters")] public LoadType loadType;
    }
}