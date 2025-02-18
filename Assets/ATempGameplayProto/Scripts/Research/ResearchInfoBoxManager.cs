using System;
using ScriptableObjects;
using TMPro;
using Train.Model.RollingStock;
using UnityEngine;
using UnityEngine.UI;

public class ResearchInfoBoxManager : MonoBehaviour, IInfoBoxHoverHandler
{
    [Header("UI Elements")] 
    public Image researchBackground;
    public TextMeshProUGUI researchName;
    public TextMeshProUGUI researchDescription;
    public RectTransform researchUnlockSection;
    public TextMeshProUGUI researchCost;
    public Button researchButton;
    
    private SO_RollingStock _soRollingStock;
    
    private void Awake()
    {
        _soRollingStock = GetComponentInParent<ResearchItemManager>().soRollingStock;
    }

    private void OnEnable()
    {
        _soRollingStock.research.onResearchLocked += SetLockResearchUI;
        _soRollingStock.research.onResearchUnlocked += SetUnlockResearchUI;
        _soRollingStock.research.onResearchFinished += SetFinishResearchUI;
    }

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        researchName.text = _soRollingStock.Name;
        researchDescription.text = GetResearchDescription();
        researchCost.text = $"Cost: {_soRollingStock.research.unlockCost:N0} RP";
        
        switch (_soRollingStock.research.ResearchState)
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

    private string GetResearchDescription()
    {
        string cargoType = "";
        string wagonType = "Locomotive";
        if (_soRollingStock.Type == RollingStockType.Wagon)
        {
            if((_soRollingStock as SO_Wagon).loadType == LoadType.Passengers)
            {
                cargoType = "Passenger ";
            }
            else
            {
                cargoType = "Freight ";
            }
            wagonType = "Wagon";
        }
        return $"{cargoType}{wagonType}";
    }

    public void Research()
    {
        _soRollingStock.research.FinishResearch();
    }

    private void SetFinishResearchUI()
    {
        researchBackground.color = SO_GameParameters.I.achievedColor;
        
        researchButton.interactable = false;
        
        researchButton.GetComponentInChildren<TextMeshProUGUI>().text = "Unlocked";
    }
    
    private void SetUnlockResearchUI()
    {
        researchBackground.color = SO_GameParameters.I.availableColor;
        
        researchUnlockSection.gameObject.SetActive(true);
        researchButton.interactable = true;
    }
    
    private void SetLockResearchUI()
    {
        researchBackground.color = SO_GameParameters.I.unavailableColor;
        
        researchUnlockSection.gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _soRollingStock.research.onResearchLocked -= SetLockResearchUI;
        _soRollingStock.research.onResearchUnlocked -= SetUnlockResearchUI;
        _soRollingStock.research.onResearchFinished -= SetFinishResearchUI;
    }
}

