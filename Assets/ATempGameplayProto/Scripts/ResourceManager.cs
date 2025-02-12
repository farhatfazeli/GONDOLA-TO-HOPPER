using System;
using Persistence;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour, ISaveable
{
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
            onResourceUpdated?.Invoke();
        }
    }
    
    private static float passengerKm;
    public static float PassengerKm
    {
        get => passengerKm;
        set
        {
            passengerKm = value;
            onResourceUpdated?.Invoke();
        }
    }
    
    private static Action onResourceUpdated;

    
    public static bool SpendResources(float pKm, float tKm)
    {
        if (PassengerKm < pKm || tonneKm < tKm) return false;
        PassengerKm -= pKm;
        tonneKm -= tKm;
        return true;
    }
    
    private void UpdateResourceUI()
    {
        passengerKmAmountText.text = PassengerKm.ToString("F0").PadLeft(9, '0');
        tonneKmAmountText.text = TonneKm.ToString("F0").PadLeft(9, '0');
    }

    private void Start()
    {
        onResourceUpdated += UpdateResourceUI;
        UpdateResourceUI();
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
    }

    public void PopulateSaveData(SaveData sd)
    {
        sd.passengerKm = PassengerKm;
        sd.tonneKm = TonneKm;
    }

    public void LoadFromSaveData(SaveData sd)
    {
        PassengerKm = sd.passengerKm;
        TonneKm = sd.tonneKm;
    }
}
