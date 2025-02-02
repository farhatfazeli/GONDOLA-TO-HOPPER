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
    public Image itemImage;
    public TextMeshProUGUI availableText;
    public TMP_InputField selectedAmount;

    private void Start()
    {
        itemName.text = rollingStock.Name;
        itemImage.sprite = rollingStock.DepotSprite;
        UpdateUI();
    }

    private void UpdateUI()
    {
        availableText.text = $"Available: {rollingStock.AvailableAmount}";
    }
    
}
