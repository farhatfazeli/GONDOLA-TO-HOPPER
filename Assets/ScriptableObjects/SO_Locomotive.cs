using System;
using System.Collections.Generic;
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
    }
}