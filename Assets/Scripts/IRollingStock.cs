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
    
    [Header ("Research parameters")]
    public Research research;

    [Header("Depot parameters")]
    public Depot depot;

    [Header ("Sprite parameters")]
    public Sprite depotSprite;

    private void OnEnable()
    {
        Initialize();
    }
    
    private void Initialize()
    {
        research.Initialize();
        depot.Initialize(research);
    }

    public void Reset()
    {
        research.LockResearch();
        depot.Reset();
    }
    
    private void OnDisable()
    {
        research.onResearchFinished -= depot.ListInDepot;
    }
}

public enum RollingStockType
{
    Locomotive,
    Wagon,
}