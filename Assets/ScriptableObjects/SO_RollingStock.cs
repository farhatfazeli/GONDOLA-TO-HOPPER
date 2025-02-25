using System;
using ATempGameplayProto.Scripts.Research;
using UnityEngine;

namespace ScriptableObjects
{
    public abstract class SO_RollingStock : ScriptableObject, IRollingStock
    {
        public string uuid;
        public string Name => name; // Unity allows you to use the asset name
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
        //
        // private void OnEnable()
        // {
        //     Initialize();
        // }
        //
        // private void Initialize()
        // {
        //     research.Initialize();
        //     depot.Initialize(research);
        // }
        //
        // public void Reset()
        // {
        //     research.LockResearch();
        //     depot.Reset();
        // }
        //
        // private void OnDisable()
        // {
        //     research.onResearchFinished -= depot.ListInDepot;
        // }
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