using ScriptableObjects;
using UnityEngine;

namespace EconomySystem
{
    public class EconomyController : MonoBehaviour
    {
        public Economy economy;

        private void Awake()
        {
            economy.Initialize();
        }
        
        public void ProcessArrival(object data)
        {
            if (data is FreightData freightData)
            {
                float amount = freightData.Amount;
                Debug.Log($"Freight amount: {amount}");
                economy.AddFreight(amount);
            }
            else
            {
                Debug.LogError("Invalid data type received.");
            }
        }
    }
    
    public class FreightData
    {
        public float Amount { get; private set; }
    
        public FreightData(float amount)
        {
            Amount = amount;
        }
    }
}