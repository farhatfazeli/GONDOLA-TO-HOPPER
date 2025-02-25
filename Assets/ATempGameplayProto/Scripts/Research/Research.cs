using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

[Serializable]
public class Research
{
    [SerializeField] private ResearchState researchState;
    public ResearchState ResearchState
    {
        get => researchState;
        private set => researchState = value;
    }

    public int unlockCost;
    public List<SO_RollingStock> previousResearch;
    
    public Action onResearchLocked;
    public Action onResearchUnlocked;
    public Action onResearchFinished;

    public void Initialize()
    {
        if (previousResearch is null or {Count: 0})
        {
            FinishResearch();
        }
        else
        {
            CheckPreviousResearch();
            SubscribeToPreviousResearch();
        }
    }

    private void SubscribeToPreviousResearch()
    {
        foreach (var research in previousResearch)
        {
            research.research.onResearchFinished += CheckPreviousResearch;
        }
    }

    private void CheckPreviousResearch()
    {
        if (previousResearch.TrueForAll(r => r.research.ResearchState == ResearchState.Researched))
        {
            UnlockResearch();
        }
        else
        {
            LockResearch();
        }
    }

    public void FinishResearch()
    {
        ResearchState = ResearchState.Researched;
        onResearchFinished?.Invoke();
    }

    public void UnlockResearch()
    {
        ResearchState = ResearchState.Researchable;
        onResearchUnlocked?.Invoke();
    }
    
    public void LockResearch()
    {
        ResearchState = ResearchState.UnResearchable;
        onResearchLocked?.Invoke();
    }
}

public enum ResearchState
{
    Researched,
    Researchable,
    UnResearchable
}