using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Research
{
    public AchievementState achievementState;
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
        if (previousResearch.TrueForAll(r => r.research.achievementState == AchievementState.Achieved))
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
        achievementState = AchievementState.Achieved;
        onResearchFinished?.Invoke();
    }

    public void UnlockResearch()
    {
        achievementState = AchievementState.Available;
        onResearchUnlocked?.Invoke();
    }
    
    public void LockResearch()
    {
        achievementState = AchievementState.Unavailable;
        onResearchLocked?.Invoke();
    }
}

public enum AchievementState
{
    Achieved,
    Available,
    Unavailable
}