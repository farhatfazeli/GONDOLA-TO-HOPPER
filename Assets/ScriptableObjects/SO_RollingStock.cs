using System;
using ATempGameplayProto.Scripts.Research;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ScriptableObjects
{
    public abstract class SO_RollingStock : ScriptableObject, IRollingStock
    {
        public string uuid;
        
        public abstract RollingStockType Type { get; }

        [Header("Rolling stock parameters")] public int mass;

        [Header("Research parameters")] public Research research;

        [Header("Yard parameters")] 
        public int startingAmount;
        public int purchaseCost;

        [Header("Sprite parameters")] 
        public Sprite yardSprite;
        public Sprite researchSprite;
        public GameObject viewGo;
    

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(uuid))
            {
                uuid = Guid.NewGuid().ToString();
            }
        }
    }

    public enum RollingStockType
    {
        Locomotive,
        Wagon
    }

    public interface IRollingStock
    {
    }
}