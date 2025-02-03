using System.Collections.Generic;
using UnityEngine;

public interface IRollingStock
{
    string Name { get; }
    RollingStockType Type { get; }
    AchievementState AchievementState { get; set; }
    int UnlockCost { get; }
    int Mass { get; }
    Sprite DepotSprite { get; }
    int AvailableAmount { get; }
}

public abstract class RollingStock : ScriptableObject, IRollingStock
{
    public string Name => name;  // Unity allows you to use the asset name
    public abstract RollingStockType Type { get; }
    public abstract AchievementState AchievementState { get; set; }
    public abstract int UnlockCost { get; }
    public abstract int Mass { get; }
    public abstract Sprite DepotSprite { get; }
    public abstract int AvailableAmount { get; set; }
}

public enum RollingStockType
{
    Locomotive,
    Wagon,
}