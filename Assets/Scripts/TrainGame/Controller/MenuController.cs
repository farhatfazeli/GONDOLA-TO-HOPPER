using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrainGame.Controller
{
    public class MenuController : MonoBehaviour
    {
        public void LoadUIScene()
        {
            // Replace "MainScene" with the exact name of your main scene asset.
            SceneManager.LoadScene("SCENE_UI");
        }
    }
}