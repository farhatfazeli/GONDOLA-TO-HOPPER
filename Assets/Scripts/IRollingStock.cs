using System;
using System.Collections.Generic;
using UnityEngine;

public interface IRollingStock
{
    
}

public abstract class RollingStock : ScriptableObject, IRollingStock
{
    public string Name => name;  // Unity allows you to use the asset name
    public abstract RollingStockType Type { get; }
    
    [Header ("Rolling stock parameters")]
    public int mass;
    public int availableAmount;
    
    
    [Header ("Research parameters")]
    public Research research;

    [Header ("Sprite parameters")]
    public Sprite depotSprite;

    private void OnEnable()
    {
        research.Initialize();
    }

    public void Reset()
    {
        availableAmount = 0;
        research.LockResearch();

        if (Name is "LocomotionNr1" or "FreightWagon1")
        {
            availableAmount = 1;
        }
    }
}

public enum RollingStockType
{
    Locomotive,
    Wagon,
}