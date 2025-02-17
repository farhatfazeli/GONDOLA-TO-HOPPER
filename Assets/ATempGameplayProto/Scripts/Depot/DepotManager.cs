using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TMPro;
using Train;
using Train.Infrastructure;
using Train.Model;
using UnityEditor;
using UnityEngine;

public class DepotManager : MonoBehaviour
{
    // public TMP_InputField trainNumberInput;
    // public TMP_InputField trainNameInput;
    //
    // public Transform locomotiveTransform;
    // public Transform wagonTransform;
    //
    // public void BuildTrain()
    // {
    //     List<RollingStock> rollingStockSelection = IdentifyPlayerSelection();
    //     string trainName = $"{trainNumberInput.text} {trainNameInput.text}";
    //     
    //     TrainConsistModel trainConsist = TrainConsistFactory.CreateTrainConsist(trainName, rollingStockSelection);
    //     
    //     RailwayDirector.Instance.AddTrainConsist(trainConsist);
    //     //ReduceRollingStock();
    // }
    //
    // private List<RollingStock> IdentifyPlayerSelection()
    // {
    //     List<DepotItemManager> depotItems = GetDepotItems();
    //     List<RollingStock> rollingStockSelection = new List<RollingStock>();
    //
    //     foreach(DepotItemManager depotItem in depotItems)
    //     {
    //         int selectedAmount = ParseSelectedAmount(depotItem.selectedAmount);
    //         selectedAmount = Mathf.Min(selectedAmount, depotItem.rollingStock.depot.AvailableAmount);
    //
    //         if (selectedAmount > 0)
    //         {
    //             rollingStockSelection.AddRange(Enumerable.Repeat(depotItem.rollingStock, selectedAmount));
    //         }
    //     }
    //     
    //     return rollingStockSelection;
    // }
    //
    // private List<DepotItemManager> GetDepotItems()
    // {
    //     List<DepotItemManager> depotItems = new List<DepotItemManager>();
    //     depotItems.AddRange(GetDepotItemsFromTransform(locomotiveTransform));
    //     depotItems.AddRange(GetDepotItemsFromTransform(wagonTransform));
    //     return depotItems;
    // }
    //
    // private List<DepotItemManager> GetDepotItemsFromTransform(Transform parentTransform)
    // {
    //     return parentTransform.Cast<Transform>()
    //         .Select(child => child.GetComponent<DepotItemManager>())
    //         .Where(depotItem => depotItem != null)
    //         .ToList();
    // }
    //
    // private int ParseSelectedAmount(TMP_InputField selectedAmount)
    // {
    //     return int.TryParse(selectedAmount.text, out int result) ? result : 0;
    // }
}