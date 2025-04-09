using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TrainGame;
using UnityEngine;
using Utility;

namespace Persistence
{
    public class SaveManager : PersistentSingleton<SaveManager>
    {
        [SerializeField]private SaveData saveData;
    
        private List<ISaveable> _saveables;
    
        private IDataService _dataService;
    
        public bool IsLoadPhaseOver { get; private set; }

        protected void Start()
        {
            base.Awake();
            _dataService = new FileDataService(new JsonSerializer());
            StartCoroutine(WaitAndLoad());
        }

        private System.Collections.IEnumerator WaitAndLoad()
        {
            // Wait until RailwayDirector is initialized.
            while (!RailwayDirector.I.IsInitialized)
                yield return null;
            
            _saveables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>().ToList();
            
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public void SaveGame()
        {
            Debug.Log("SaveManager: saving game");
            saveData = new SaveData();
            foreach (ISaveable saveable in _saveables)
            {
                Debug.Log("In foreach loop: "  + saveable.GetType().Name);
                saveable.PopulateSaveData(saveData);
            }
            _dataService.Save(saveData);
        }

        public void LoadGame()
        {
            saveData = _dataService.Load(SO_GameParameters.I.saveFileName);
            if (saveData == null)
            {
                IsLoadPhaseOver = true;
                return;
            }
            foreach (ISaveable saveable in _saveables)
            {
                saveable.LoadFromSaveData(saveData);
            }
        
            IsLoadPhaseOver = true;
        }
    }
}
