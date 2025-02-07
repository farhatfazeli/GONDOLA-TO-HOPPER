using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : PersistentSingleton<SaveManager>
{
    private SaveData _saveData;
    
    private List<ISaveable> _saveables;
    
    private IDataService _dataService;

    protected override void Awake()
    {
        base.Awake();
        _dataService = new FileDataService(new JsonSerializer());
        _saveables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>().ToList();
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void SaveGame()
    {
        _saveData = new SaveData();
        foreach (ISaveable saveable in _saveables)
        {
            saveable.PopulateSaveData(_saveData);
        }
        _dataService.Save(_saveData);
    }

    private void LoadGame()
    {
        _saveData = _dataService.Load(SO_GameParameters.I.saveFileName);
        foreach (ISaveable saveable in _saveables)
        {
            saveable.LoadFromSaveData(_saveData);
        }
    }
}   

// public class SaveManager : MonoBehaviour
// {
//     private List<ISaveable> _saveables;
//     
//     private void Start()
//     {
//         _saveables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>().ToList();
//         ReadJsonData();
//     }
//
//     private void OnApplicationQuit()
//     {
//         SaveJsonData();
//     }
//
//     private void SaveJsonData()
//     {
//         SaveData sd = new SaveData();
//         foreach (ISaveable saveable in _saveables)
//         {
//             saveable.PopulateSaveData(sd);
//         }
//         
//         string json = sd.ToJson(); 
//         WriteToFile(SO_GameParameters.I.saveFileName, json);
//     }
//
//     private void ReadJsonData()
//     {
//         ReadFromFile(SO_GameParameters.I.saveFileName, out string json);
//         SaveData sd = new SaveData();
//         sd.FromJson(json);
//         
//         foreach (ISaveable saveable in _saveables)
//         {
//             saveable.LoadFromSaveData(sd);
//         }
//     }
//
//     private static bool WriteToFile(string fileName, string fileContents)
//     {
//         var fullPath = Path.Combine(Application.persistentDataPath, fileName);
//         try
//         {
//             File.WriteAllText(fullPath, fileContents);
//             return true;
//         }
//         catch (IOException e)
//         {
//             Debug.LogError($"Failed to write to file {fullPath} with exception {e}");
//             return false;
//         }
//     }
//     
//     private static bool ReadFromFile(string fileName, out string fileContents)
//     {
//         var fullPath = Path.Combine(Application.persistentDataPath, fileName);
//         try
//         {
//             fileContents = File.ReadAllText(fullPath);
//             return true;
//         }
//         catch (IOException e)
//         {
//             Debug.LogError($"Failed to read from file {fullPath} with exception {e}");
//             fileContents = "";
//             return false;
//         }
//     }
// }
