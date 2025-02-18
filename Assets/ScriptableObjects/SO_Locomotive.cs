using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLocomotive", menuName = "TrainGame/Locomotive")]
    public class SO_Locomotive : SO_RollingStock
    {
        public override RollingStockType Type => RollingStockType.Locomotive;

        [Header("Physics parameters")]
        public float maxSpeed;
        public float tractionCoefficient;
        public float brakingCoefficient;
    }
}