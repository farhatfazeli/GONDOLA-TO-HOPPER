using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class UILandscapePanelHandler : MonoBehaviour
    {
        public Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void LoadLandscape(Landscape landscape)
        {
            image.sprite = landscape.image;
        }
    }
}