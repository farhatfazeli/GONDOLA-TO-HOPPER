using System.Collections.Generic;
using System.Globalization;
using ScriptableObjects;
using TMPro;
using UnityEngine;

public class TrainDropdownManager : MonoBehaviour
{
    public List<TempTrain> trains;

    public TempTrain selectedTrain;

    [Header("Infos")]
    public TextMeshProUGUI tractionPowerText;
    public TextMeshProUGUI maxSpeedText;
    
    public void OnTrainSelected(int index)
    {
        selectedTrain = trains[index];
        UpdateInfos();
    }

    private void UpdateInfos()
    {
        tractionPowerText.text = $"<i>Traction power: {selectedTrain.tractionCoefficient/1000:F1} kN</i>";
        maxSpeedText.text = $"<i>Max speed: {selectedTrain.maxSpeed:F1} km/h</i>";
    }

    private void Start()
    {
        selectedTrain = trains[0];
        UpdateInfos();
    }
}
