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
        private YardMarshallController _yardMarshallController;
        private RollingStockModel _rollingStockModel;
        public SO_RollingStock soRollingStock;
    
        [Header("UI Elements")]
        public Image itemImage;
        public TextMeshProUGUI itemDescription;
        public Button selectButton;
        public Button purchaseButton;
        
        
        
        public void Initialize(YardMarshallController ymc, RollingStockModel ro, SO_RollingStock so)
        {
            _yardMarshallController = ymc;
            _rollingStockModel = ro;
            soRollingStock = so;
            itemImage.sprite = soRollingStock.yardSprite;
            _rollingStockModel.onModelChanged += UpdateView;
            UpdateView();
        }
        
        private void UpdateView()
        {
            HandleDescriptionText();
        }
        
        private void HandleDescriptionText()
        {
            itemDescription.text = $"{_rollingStockModel.name} ({_rollingStockModel.AvailableAmount}/{_rollingStockModel.FleetAmount})";
        }
        
        public void OnSelectButtonClicked()
        {
            _yardMarshallController.SelectRollingStock(_rollingStockModel);
        }
        
        public void OnPurchaseButtonClicked()
        {
            _yardMarshallController.PurchaseRollingStock(_rollingStockModel);
        }
    }
}