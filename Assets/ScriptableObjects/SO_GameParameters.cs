using UnityEngine;

[CreateAssetMenu(fileName = "SO_GameParameters", menuName = "Scriptable Objects/SO_GameParameters")]
public class SO_GameParameters : ScriptableObject
{
    [Header("Speed up parameters")]
    public int gameSpeedUpFactor = 1;
    public float loadSpeedUpFactor = 1f;
    public float travelSpeedUpFactor = 1f;

    [Header("Click amount parameters")] 
    public float loadClickProgressAmount = 0.1f;
    public float travelClickProgressAmount = 0.1f;
    public float unloadClickProgressAmount = 0.1f;
    
    [Header("Folders")]
    public string locomotivesFolderPathShort = "Data/RollingStock/Locomotives"; // Relative to 'Resources'
    public string wagonsFolderPathShort = "Data/RollingStock/Wagons"; // Relative to 'Resources'
    
    [Header("Gameplay settings")]
    public string addressableLabelStations = "Stations";
    public string addressableLabelRollingStock = "RollingStock";
    
    [Header("UI settings")] 
    public Color32 achievedColor = new Color32(0xF6, 0xFF, 0xAA, 0xFF);
    public Color32 availableColor = new Color32(0xFE, 0xFF, 0xF6, 0xFF);
    public Color32 unavailableColor = new Color32(0x7F, 0x7F, 0x7F, 0xFF);
    
    [Header("Save settings")]
    public string saveFileName = "save";
    public string fileExtension = "json";
    
    public static SO_GameParameters I { get; private set; }
    
    private void OnEnable()
    {
        I = this;
    }
}
