using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StationInfoBoxManager : MonoBehaviour
{
    public Station station;
    
    [Header("UI Elements")]
    public TextMeshProUGUI stationName;
    public TextMeshProUGUI resourceCost;
    public Button buildButton;
    
    private void Start()
    {
        stationName.text = station.stationName;
        resourceCost.text = $"Cost: {station.resourceCost:N0} tonne-kilometers";
        buildButton.onClick.AddListener(BuildStation);
    }
    
    private void BuildStation()
    {
        // Build the station
    }
}
