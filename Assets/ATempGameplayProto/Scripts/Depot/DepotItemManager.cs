using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepotItemManager : MonoBehaviour
{
    public RollingStock rollingStock;
    
    [Header("UI Elements")]
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI availableText;
    public Image itemImage;
    public TMP_InputField selectedAmount;
    public RectTransform interactPanel;
    public Button purchaseButton;
    public RectTransform greyOutPanel;

    private void OnEnable()
    {
        rollingStock.research.onResearchFinished += SetUsable;
    }

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        itemName.text = rollingStock.Name;
        itemImage.sprite = rollingStock.depotSprite;
        CheckAchievementState();
        CheckAvailability();
        UpdateUI();
    }
    
    private void CheckAchievementState()
    {
        switch (rollingStock.research.AchievementState)
        {
            case AchievementState.Achieved:
                SetBuyable();
                break;
            case AchievementState.Unavailable:
                SetUnavailable();
                break;
            case AchievementState.Available:
                SetUnavailable();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CheckAvailability()
    {
        switch (rollingStock.availableAmount)
        {
            case 0:
                SetUnavailable();
                break;
            default:
                SetUsable();
                break;
        }
    }

    private void SetUnavailable()
    {
        interactPanel.gameObject.SetActive(false);
        purchaseButton.interactable = false;
        greyOutPanel.gameObject.SetActive(true);
    }
    
    private void SetBuyable()
    {
        interactPanel.gameObject.SetActive(false);
        purchaseButton.interactable = true;
        greyOutPanel.gameObject.SetActive(true);
    }

    private void SetUsable()
    {
        interactPanel.gameObject.SetActive(true);
        purchaseButton.interactable = true;
        greyOutPanel.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        availableText.text = $"Available: {rollingStock.availableAmount}";
    }

    public void Purchase()
    {
        rollingStock.availableAmount++;
        UpdateUI();
    }
    
}
