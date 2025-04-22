using System;
using UnityEngine;

[Serializable]
public class Depot
{
    [SerializeField] private int availableAmount;
    [SerializeField] private DepotState depotState;
    public int AvailableAmount
    {
        get => availableAmount;
        private set
        {
            availableAmount = value;
            CheckDepotState();
        }
    }
    
    public DepotState DepotState
    {
        get => depotState;
        private set
        {
            depotState = value;
            onDepotStateChanged?.Invoke();
        }
    }
    
    public int purchaseCost;
    
    public Action onDepotStateChanged;
    
    private Research _research;

    public void Initialize(Research research)
    {
        _research = research;
        if(_research.ResearchState == ResearchState.Researched)
        {
            ListInDepot();
        }
        _research.onResearchFinished += ListInDepot;
    }
    
    private void CheckDepotState()
    {
        if(_research.ResearchState == ResearchState.UnResearchable)
        {
            DepotState = DepotState.Unavailable;
        }
        else
        {
            DepotState = AvailableAmount == 0 ? DepotState.NotInDepot : DepotState.InDepot;
        }
    }

    private void AddToDepot(int amount = 1)
    {
        AvailableAmount += amount;
    }
    
    private void RemoveFromDepot(int amount = 1)
    {
        AvailableAmount -= amount;
        AvailableAmount = Mathf.Max(0, AvailableAmount);
    }

    public void Purchase()
    {
        if (depotState == DepotState.Unavailable) return;
        if(ResosurceManager.SpendResources(0, purchaseCost))
        {
            AddToDepot();
        }
    }

    public void ListInDepot()
    {
        DepotState = DepotState.InDepot;
        AvailableAmount = 1;
    }
    
    public void Reset()
    {
        DepotState = DepotState.Unavailable;
        AvailableAmount = 0;
    }
    
}

public enum DepotState
{
    Unavailable,
    NotInDepot,
    InDepot
}
