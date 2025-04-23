using System;
using ScriptableObjects;
using TMPro;
using TrainGame.Controller;
using TrainGame.Model.RollingStock;
using UnityEngine;
using UnityEngine.UI;

namespace TrainGame.View.YardView
{
    public class YardMarshallItemView : MonoBehaviour
    {
        
        [Header("UI Elements")]
        [SerializeField] private Image background;
        [SerializeField] private Image priceBackground;
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemAvailability;
        [SerializeField] private TextMeshProUGUI itemPrice;

        [Header("Interaction UI elements")]
        [SerializeField] private Button selectButton;
        [SerializeField] private Button purchaseButton;

        [Header("Visual options")] 
        [SerializeField] private Sprite locomotiveBackground;
        [SerializeField] private Sprite passengerWagonBackground;
        [SerializeField] private Sprite freightWagonBackground;
        [SerializeField] private Sprite locomotivePriceBackground;
        [SerializeField] private Sprite passengerWagonPriceBackground;
        [SerializeField] private Sprite freightWagonPriceBackground;
        
        private RollingStockModel _rollingStockModel;
        private YardMarshallFilter _filter;
        
        public void Initialize(RollingStockModel ro, YardMarshallController ymc, YardMarshallFilter filter)
        {
            _rollingStockModel = ro;
            _filter = filter;
            _rollingStockModel.OnModelChanged += RefreshView;
            RefreshView();

            HandleFixedDetails();
            HandleBackground();
            
            itemImage.sprite = RollingStockManager.I.QueryService.GetSo(ro).yardSprite;
            
            selectButton.onClick.AddListener(() => ymc.SelectRollingStock(ro));
            purchaseButton.onClick.AddListener(() => ymc.PurchaseRollingStock(ro));
        }
        
        private void RefreshView()
        {
            HandleAvailabilityText();
        }

        private void HandleFixedDetails()
        {
            itemName.text = _rollingStockModel.name;
            itemPrice.text = _rollingStockModel.purchaseCost.ToString("N0");
        }
        
        private void HandleBackground()
        {
            background.sprite = _filter switch
            {
                YardMarshallFilter.Locomotives => locomotiveBackground,
                YardMarshallFilter.PassengerWagons => passengerWagonBackground,
                YardMarshallFilter.FreightWagons => freightWagonBackground,
                _ => throw new ArgumentOutOfRangeException()
            };

            priceBackground.sprite = _filter switch
            {
                YardMarshallFilter.Locomotives => locomotivePriceBackground,
                YardMarshallFilter.PassengerWagons => passengerWagonPriceBackground,
                YardMarshallFilter.FreightWagons => freightWagonPriceBackground,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void HandleAvailabilityText()
        {
            itemAvailability.text = $"({_rollingStockModel.AvailableAmount}/{_rollingStockModel.FleetAmount})";
        }

        private void OnDestroy()
        {
            selectButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.RemoveAllListeners();
        }
    }
}