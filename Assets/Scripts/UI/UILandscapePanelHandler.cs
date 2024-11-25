using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
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