using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepotItemManager : MonoBehaviour
{
    // public SO_RollingStock soRollingStock;
    //
    // [Header("UI Elements")]
    // public TextMeshProUGUI itemName;
    // public TextMeshProUGUI availableText;
    // public Image itemImage;
    // public TMP_InputField selectedAmount;
    // public RectTransform interactPanel;
    // public Button purchaseButton;
    // public RectTransform greyOutPanel;
    //
    // private void OnEnable()
    // {
    //     //soRollingStock.depot.onDepotStateChanged += CheckAvailability;
    // }
    //
    // private void Start()
    // {
    //     InitializeUI();
    // }
    //
    // private void InitializeUI()
    // {
    //     itemName.text = soRollingStock.Name;
    //     itemImage.sprite = soRollingStock.yardSprite;
    //     CheckAvailability();
    // }
    //
    // private void CheckAvailability()
    // {
    //     switch (soRollingStock.depot.DepotState)
    //     {
    //         case DepotState.Unavailable:
    //             SetUnavailable();
    //             break;
    //         case DepotState.NotInDepot:
    //             SetBuyable();
    //             break;
    //         case DepotState.InDepot:
    //             SetUsable();
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException();
    //     }
    // }
    //
    // private void SetUnavailable()
    // {
    //     UpdateUI(interactable: false, purchaseable: false, greyedOut: true);
    // }
    //
    // private void SetBuyable()
    // {
    //     UpdateUI(interactable: false, purchaseable: true, greyedOut: true);
    // }
    //
    // private void SetUsable()
    // {
    //     UpdateUI(interactable: true, purchaseable: true, greyedOut: false);
    // }
    //
    // private void UpdateUI(bool interactable, bool purchaseable, bool greyedOut)
    // {
    //     interactPanel.gameObject.SetActive(interactable);
    //     purchaseButton.gameObject.SetActive(purchaseable);
    //     greyOutPanel.gameObject.SetActive(greyedOut);
    //     availableText.text = $"Available: {soRollingStock.depot.AvailableAmount}";
    //
    // }
    //
    // public void Purchase()
    // {
    //     soRollingStock.depot.Purchase();
    //     SetUsable();
    // }
    
}
