using TrainGame;
using UnityEngine;

namespace Core.Utility
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute()
        {
            // Object.DontDestroyOnLoad(Addressables.InstantiateAsync("Assets/Prefabs/System/System.prefab")
            //     .WaitForCompletion());
            _ = GameSetup.RunStartupSequence().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Debug.LogException(task.Exception.Flatten());
                }
            });
        }
    }
}