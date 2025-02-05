using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchItemManager : MonoBehaviour
{
    public RollingStock rollingStock;
    
    [Header("UI Elements")]
    public Image researchSnippetBackground;
    public TextMeshProUGUI researchSnippetName;
    
    private void OnEnable()
    {
        rollingStock.research.onResearchLocked += SetLockResearchUI;
        rollingStock.research.onResearchUnlocked += SetUnlockResearchUI;
        rollingStock.research.onResearchFinished += SetFinishResearchUI;
    }

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        researchSnippetName.text = rollingStock.Name;

        CheckAchievementState();
    }

    private void CheckAchievementState()
    {
        switch (rollingStock.research.ResearchState)
        {
            case ResearchState.Researched:
                SetFinishResearchUI();
                break;
            case ResearchState.UnResearchable:
                SetLockResearchUI();
                break;
            case ResearchState.Researchable:
                SetUnlockResearchUI();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetFinishResearchUI()
    {
        researchSnippetBackground.color = SO_GameParameters.I.achievedColor;
    }
    
    private void SetLockResearchUI()
    {
        researchSnippetBackground.color = SO_GameParameters.I.unavailableColor;
    }
    
    private void SetUnlockResearchUI()
    {
        researchSnippetBackground.color = SO_GameParameters.I.availableColor;
    }
    
    private void OnDisable()
    {
        rollingStock.research.onResearchLocked -= SetLockResearchUI;
        rollingStock.research.onResearchUnlocked -= SetUnlockResearchUI;
        rollingStock.research.onResearchFinished -= SetFinishResearchUI;
    }
}
