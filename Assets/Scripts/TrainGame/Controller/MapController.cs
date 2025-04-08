using Persistence;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrainGame.Controller
{
    public class MapController : MonoBehaviour
    {
        [Header("Map panel")]
        [SerializeField] private RectTransform mapPanel;
        
        public void OnActivate()
        {
            mapPanel.gameObject.SetActive(true);
            LoadMapScene();
            StartCoroutine(WaitAndDo());
        }
        
        public void OnDeactivate()
        {
            mapPanel.gameObject.SetActive(false);
            UnloadMapScene();
        }

        private void LoadMapScene()
        {
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(SO_GameParameters.I.mapScene, LoadSceneMode.Additive);
            if (asyncOp != null) asyncOp.completed += operation => OnMapSceneLoaded();
        }
        
        private static void UnloadMapScene()
        {
            Scene mapScene  = SceneManager.GetSceneByName(SO_GameParameters.I.mapScene);
            
            if (mapScene.isLoaded)
                SceneManager.UnloadSceneAsync(mapScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

        private static System.Collections.IEnumerator WaitAndDo()
        {
            do
            {
                yield return null;
            } while (!RailwayDirector.I.IsInitialized || !SaveManager.I.IsLoadPhaseOver);
        }
        
        private static void OnMapSceneLoaded()
        {

        }
    }
}