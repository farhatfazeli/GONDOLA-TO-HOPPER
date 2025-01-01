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
        
        private void Update()
        {
            economy.PeriodicUpdate();
        }
        
        public void ProcessArrival(object data)
        {
            if (data is FreightData freightData)
            {
                float amount = freightData.Amount;
                Debug.Log($"Freight amount: {amount}");
                economy.DeliverFreight(amount);
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