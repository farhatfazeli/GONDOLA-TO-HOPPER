using System;
using System.Threading.Tasks;
using ScriptableObjects;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Core.Utility
{
    public static class GameSetup
    {
        public static async Task RunStartupSequence()
        {
            await LoadGameParameters();
            await LoadAndPersistSystemPrefab();
        }

        private static async Task LoadGameParameters()
        {
            var handle = Addressables.LoadAssetAsync<SO_GameParameters>("GameParameters");
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new SystemException("Failed to load game parameters.");
        }

        private static async Task LoadAndPersistSystemPrefab()
        {
            var handle = Addressables.InstantiateAsync("System");
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new SystemException("Failed to instantiate system prefab.");

            Object.DontDestroyOnLoad(handle.Result);

        }
    }
}