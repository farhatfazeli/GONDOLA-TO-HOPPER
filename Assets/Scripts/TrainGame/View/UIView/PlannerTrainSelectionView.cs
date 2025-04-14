using System.Collections.Generic;
using System.Linq;
using TMPro;
using TrainGame.Controller;
using TrainGame.Model.Service;
using TrainGame.Model.TrainConsist;
using UnityEngine;

namespace TrainGame.View.UIView
{
    public class PlannerTrainSelectionView : MonoBehaviour
    {
        [Header("UI Elements")] 
        public TMP_Dropdown trainDropdown;
        
        public SchedulerController schedulerController;
        
        private ServiceManager _serviceManager;
        
        private void RefreshUI()
        {
            HashSet<TrainConsistModel> trains = _serviceManager.GetTrainConsistsOnStandby();
            List<string> options = trains.Select(x => x.name).ToList();
            trainDropdown.ClearOptions();
            trainDropdown.AddOptions(options);
        }
        
        private void Awake()
        {
            trainDropdown = GetComponent<TMP_Dropdown>();
            _serviceManager = ServiceManager.I;
        }
        
        private void OnEnable()
        {
            _serviceManager.OnServiceListUpdated += RefreshUI;
        }

        private void Start()
        {

            RefreshUI();
        }

        private void OnDisable()
        {
            _serviceManager.OnServiceListUpdated -= RefreshUI;
        }
    }
}