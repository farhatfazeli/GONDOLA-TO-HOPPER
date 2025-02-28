using System;
using ATempGameplayProto.Scripts.Research;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchItemManager : MonoBehaviour
{
    public SO_RollingStock soRollingStock;
    
    [Header("UI Elements")]
    public Image researchSnippetBackground;
    public TextMeshProUGUI researchSnippetName;
    
    private void OnEnable()
    {
        soRollingStock.research.onResearchLocked += SetLockResearchUI;
        soRollingStock.research.onResearchUnlocked += SetUnlockResearchUI;
        soRollingStock.research.onResearchFinished += SetFinishResearchUI;
    }

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        researchSnippetName.text = soRollingStock.name;

        CheckAchievementState();
    }

    private void CheckAchievementState()
    {
        switch (soRollingStock.research.ResearchState)
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
        soRollingStock.research.onResearchLocked -= SetLockResearchUI;
        soRollingStock.research.onResearchUnlocked -= SetUnlockResearchUI;
        soRollingStock.research.onResearchFinished -= SetFinishResearchUI;
    }
}
