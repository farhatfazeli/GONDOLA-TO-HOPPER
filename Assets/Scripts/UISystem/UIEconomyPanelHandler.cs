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
        public TextMeshProUGUI economyText;

        private void UpdateUI()
        {
            economyText.text = "Freight Hauled: " + economy.FreightHauled + "    " +
                               "Passengers Carried: " + economy.PassengersCarried;
        }
        private void OnEnable()
        {
            economy.onEconomyChanged += UpdateUI;
        }

        private void OnDisable()
        {
            economy.onEconomyChanged -= UpdateUI;
        }
    }
}
