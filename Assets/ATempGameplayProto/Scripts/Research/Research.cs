using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Research
{
    [SerializeField] private AchievementState achievementState;
    public AchievementState AchievementState
    {
        get => achievementState;
        private set => achievementState = value;
    }

    public int unlockCost;
    public List<RollingStock> previousResearch;
    
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
        if (previousResearch.TrueForAll(r => r.research.AchievementState == AchievementState.Achieved))
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
        AchievementState = AchievementState.Achieved;
        onResearchFinished?.Invoke();
    }

    public void UnlockResearch()
    {
        AchievementState = AchievementState.Available;
        onResearchUnlocked?.Invoke();
    }
    
    public void LockResearch()
    {
        AchievementState = AchievementState.Unavailable;
        onResearchLocked?.Invoke();
    }
}

public enum AchievementState
{
    Achieved,
    Available,
    Unavailable
}