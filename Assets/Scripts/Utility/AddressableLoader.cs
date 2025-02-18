using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utility
{
    public static class AddressableLoader<T> where T : Object
    {
        public static async Task<List<T>> LoadAllAsync(string label)
        {
            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(label, null);
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return new List<T>(handle.Result);
            }
            else
            {
                Debug.LogError($"Failed to load assets with label {label}");
                return new List<T>();
            }
        }
    }
}