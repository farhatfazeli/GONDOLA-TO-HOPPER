using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Utility
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute() =>
            Object.DontDestroyOnLoad(Addressables.InstantiateAsync("Assets/Prefabs/System/System.prefab")
                .WaitForCompletion());
    }
}