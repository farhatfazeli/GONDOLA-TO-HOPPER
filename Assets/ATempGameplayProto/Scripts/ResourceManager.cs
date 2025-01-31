using System;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public SO_GameParameters gameParameters;
    
    [Header("UI References")]
    public TextMeshProUGUI passengerKmAmountText;
    public TextMeshProUGUI tonneKmAmountText;
    
    private static float tonneKm;
    public static float TonneKm
    {
        get => tonneKm;
        set
        {
            tonneKm = value;
            onResourceUpdated?.Invoke(); // Trigger UI update if subscribed
        }
    }
    
    private static float passengerKm;
    public static float PassengerKm
    {
        get => passengerKm;
        set
        {
            passengerKm = value;
            onResourceUpdated?.Invoke(); // Trigger UI update if subscribed
        }
    }
    
    private static Action onResourceUpdated;

    private void UpdateResourceUI()
    {
        passengerKmAmountText.text = PassengerKm.ToString("F0").PadLeft(9, '0');
        tonneKmAmountText.text = TonneKm.ToString("F0").PadLeft(9, '0');
    }

    private void Start()
    {
        onResourceUpdated += UpdateResourceUI;
        PassengerKm = gameParameters.passengerKm;
        TonneKm = gameParameters.tonneKm;
    }

    private void Update()
    {
        //if u presesd
        if (Input.GetKeyDown(KeyCode.U))
        {
            //add 1 to the passenger km
            PassengerKm += 1;
            //update the UI
            UpdateResourceUI();
        }
    }
    
    private void OnDestroy()
    {
        onResourceUpdated -= UpdateResourceUI;
        gameParameters.passengerKm = PassengerKm;
        gameParameters.tonneKm = TonneKm;
    }
}
