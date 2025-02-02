using System;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DepotManager : MonoBehaviour
{
    public TMP_InputField trainNumberInput;
    public TMP_InputField trainNameInput;
    
    public Transform locomotiveTransform;
    public Transform wagonTransform;

    public void BuildTrain()
    {
        Dictionary<RollingStock, int> rollingStockSelection = IdentifyPlayerSelection();
        TempTrain train = CreateTrain(rollingStockSelection);
        CreateAsset(train);
        //ReduceRollingStock();
    }

    private TempTrain CreateTrain(Dictionary<RollingStock, int> rollingStockSelection)
    {
        TempTrain train = ScriptableObject.CreateInstance<TempTrain>();
        
        bool hasLocomotive = false;
        
        float maxSpeed = float.MaxValue;
        float tractionCoefficient = 0;
        float brakingCoefficient=  0;
        int mass = 0;
        
        foreach (KeyValuePair<RollingStock, int> selection in rollingStockSelection)
        {
            if(selection.Key is Locomotive locomotive)
            {
                hasLocomotive = true;
                maxSpeed = Mathf.Min(locomotive.maxSpeed, maxSpeed);
                tractionCoefficient += locomotive.tractionCoefficient;
                brakingCoefficient += locomotive.brakingCoefficient;
            }
            mass += selection.Key.Mass * selection.Value;
        }
        
        if (!hasLocomotive)
        {
            Debug.LogError("Train must have at least one locomotive");
            return null;
        }

        train.name = $"{trainNumberInput.text} {trainNameInput.text}";
        train.maxSpeed = maxSpeed;
        train.tractionCoefficient = tractionCoefficient;
        train.brakingCoefficient = brakingCoefficient;
        train.mass = mass;
        return train;
    }

    private Dictionary<RollingStock, int> IdentifyPlayerSelection()
    {
        List<DepotItemManager> depotItems = new List<DepotItemManager>();
        foreach (Transform child in locomotiveTransform)
        {
            DepotItemManager depotItem = child.GetComponent<DepotItemManager>();
            if (depotItem != null)
            {
                depotItems.Add(depotItem);
            }
        }
        foreach (Transform child in wagonTransform)
        {
            DepotItemManager depotItem = child.GetComponent<DepotItemManager>();
            if (depotItem != null)
            {
                depotItems.Add(depotItem);
            }
        }
        

        Dictionary<RollingStock, int> rollingStockSelection = new Dictionary<RollingStock, int>();
        
        foreach(DepotItemManager depotItem in depotItems)
        {
            int selectedAmount = ParseSelectedAmount(depotItem.selectedAmount);
            if (selectedAmount > depotItem.rollingStock.AvailableAmount)
            {
                selectedAmount = depotItem.rollingStock.AvailableAmount;
            }
            if (selectedAmount > 0)
            {
                rollingStockSelection.Add(depotItem.rollingStock, selectedAmount);
            }
        }
        
        return rollingStockSelection;
    }
    
    private int ParseSelectedAmount(TMP_InputField selectedAmount)
    {
        return (int)(float.TryParse(selectedAmount.text, out float result) ? result : 0f);
    }   

    private void CreateAsset(TempTrain train)
    {
        TempTrain newTrain = ScriptableObject.CreateInstance<TempTrain>();
        newTrain.name = train.name;
        newTrain.maxSpeed = train.maxSpeed;
        newTrain.tractionCoefficient = train.tractionCoefficient;
        newTrain.brakingCoefficient = train.brakingCoefficient;
        newTrain.mass = train.mass;
        
        string path = "Assets/ATempGameplayProto/Trains/" + newTrain.name + ".asset";
        AssetDatabase.CreateAsset(newTrain, path);
    }
}
