using UnityEngine;

[CreateAssetMenu(fileName = "SO_GameParameters", menuName = "Scriptable Objects/SO_GameParameters")]
public class SO_GameParameters : ScriptableObject
{
    [Header("Resources")]
    public float passengerKm;
    public float tonneKm;

    [Header("Speed up parameters")] 
    public float loadSpeedUpFactor = 1f;
    public float travelSpeedUpFactor = 1f;

    [Header("Click amount parameters")] 
    public float loadClickProgressAmount = 0.1f;
    public float travelClickProgressAmount = 0.1f;
    public float unloadClickProgressAmount = 0.1f;
    
    [Header("Folders")]
    public string locomotivesFolderPathShort = "Data/RollingStock/Locomotives"; // Relative to 'Resources'
    public string wagonsFolderPathShort = "Data/RollingStock/Wagons"; // Relative to 'Resources'
    public string trainsFolderPathShort = "Data/RollingStock/Trains"; // Relative to 'Resources'
    public string trainsFolderPathFull = "Assets/Resources/Data/RollingStock/Trains"; // Full path

    [Header("UI settings")] 
    public Color32 achievedColor = new Color32(0xF6, 0xFF, 0xAA, 0xFF);
    public Color32 availableColor = new Color32(0xFE, 0xFF, 0xF6, 0xFF);
    public Color32 unavailableColor = new Color32(0x7F, 0x7F, 0x7F, 0xFF);
    
    public static SO_GameParameters I { get; private set; }
    
    private void OnEnable()
    {
        I = this;
    }
}
