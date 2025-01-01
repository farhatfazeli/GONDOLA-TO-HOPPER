using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UISystem
{
    public class UIEconomyHandler : MonoBehaviour
    {
        public Economy economy;
        
        [SerializeField] private TextMeshProUGUI freightHauledText;
        [SerializeField] private TextMeshProUGUI freightHaulingText;
        [SerializeField] private TextMeshProUGUI freightHaulRateText;
        [SerializeField] private TextMeshProUGUI passengersCarriedText; 
        [SerializeField] private TextMeshProUGUI passengersCarryingText;
        [SerializeField] private TextMeshProUGUI passengerCarryRateText;

        private void UpdateUI()
        {
            freightHauledText.text = $"{(int)economy.FreightHauled:D8}";
            freightHaulingText.text = $"{(int)economy.FreightHauling:D6}";
            freightHaulRateText.text = $"{(int)economy.FreightHaulRate:D4}";
            passengersCarriedText.text = $"{economy.PassengersCarried:D8}";
            passengersCarryingText.text = $"{economy.PassengersCarrying:D6}";
            passengerCarryRateText.text = $"{economy.PassengerCarryRate:D4}";
        }
        
        private void OnEnable()
        {
            economy.OnEconomyChanged += UpdateUI;
            UpdateUI();
        }

        private void OnDisable()
        {
            economy.OnEconomyChanged -= UpdateUI;
        }
    }
}
