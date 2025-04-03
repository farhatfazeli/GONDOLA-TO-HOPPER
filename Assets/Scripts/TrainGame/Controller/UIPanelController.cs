using System;
using TrainGame.View.UIView;
using UnityEngine;

namespace TrainGame.Controller
{
    public class UIPanelController : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private YardMarshallController yardMarshallController;
        [SerializeField] private LandscapeController landscapeController;
        
        [SerializeField] private TabHandler landscapeTab;
        [SerializeField] private TabHandler yardTab;

        private void Start()
        {
            yardMarshallController.OnActivate();
            yardTab.OnTabClick();
        }

        public void GoToMainView()
        {
            
        }
        
        public void GoToYardView()
        {
            landscapeController.OnDeactivate();
            yardMarshallController.OnActivate();
            landscapeTab.Deactivate();
        }
        
        public void GoToLandscapeView()
        {
            yardMarshallController.OnDeactivate();
            landscapeController.OnActivate();
            yardTab.Deactivate();
        }

        public void GoToPlannerView()
        {
            
        }

        public void CleanView()
        {
            
        }
    }
}