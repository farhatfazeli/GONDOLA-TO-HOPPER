using System.Collections.Generic;
using System.Linq;
using Persistence;
using UnityEngine;

public class SaveManager : PersistentSingleton<SaveManager>
{
    [SerializeField]private SaveData _saveData;
    
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

    public void SaveGame()
    {
        _saveData = new SaveData();
        foreach (ISaveable saveable in _saveables)
        {
            saveable.PopulateSaveData(_saveData);
        }
        _dataService.Save(_saveData);
    }

    public void LoadGame()
    {
        _saveData = _dataService.Load(SO_GameParameters.I.saveFileName);
        foreach (ISaveable saveable in _saveables)
        {
            saveable.LoadFromSaveData(_saveData);
        }
    }
}
