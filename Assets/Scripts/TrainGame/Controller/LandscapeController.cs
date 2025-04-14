using Core.Persistence;
using ScriptableObjects;
using TrainGame.View.YardView;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrainGame.Controller
{
    public class LandscapeController : MonoBehaviour
    {
        [SerializeField] private RectTransform landscapePanel;
        
        public void OnActivate()
        {
            landscapePanel.gameObject.SetActive(true);
            LoadLandscapeScene();
            StartCoroutine(WaitAndDo());
        }

        public void OnDeactivate()
        {
            landscapePanel.gameObject.SetActive(false);
            UnloadLandscapeScene();
        }

        private void LoadLandscapeScene()
        {
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(SO_GameParameters.I.landscapeScene, LoadSceneMode.Additive);
            if (asyncOp != null) asyncOp.completed += operation => OnLandscapeSceneLoaded();
        }
        
        private void UnloadLandscapeScene()
        {
            Scene landscapeScene = SceneManager.GetSceneByName(SO_GameParameters.I.landscapeScene);
            
            if (landscapeScene.isLoaded)
                SceneManager.UnloadSceneAsync(landscapeScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }
        
        
        
        private System.Collections.IEnumerator WaitAndDo()
        {
            do
            {
                yield return null;
            } while (!RailwayDirector.I.IsInitialized || !SaveManager.I.IsLoadPhaseOver);
            
            //yardMarshallItemListView.Populate();
            
            OnLandscapeSceneLoaded();
        }
        
        private void OnLandscapeSceneLoaded()
        {

        }
    }
}