using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TrainGame.View.LandscapeView
{
    public class ManualBuildZone: MonoBehaviour
    {
        [SerializeField] private LandscapeRailsViewFactory landscapeRailsViewFactory;

        private void OnMouseDown()
        {
            landscapeRailsViewFactory.OnTrackClicked();
        }
    }
}