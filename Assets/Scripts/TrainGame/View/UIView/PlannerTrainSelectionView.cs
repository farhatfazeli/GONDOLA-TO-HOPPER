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
        
        private void RefreshUI()
        {
            HashSet<TrainConsistModel> trains = ServiceManager.I.QueryService.GetTrainConsistsOnStandby();
            List<string> options = trains.Select(x => x.name).ToList();
            trainDropdown.ClearOptions();
            trainDropdown.AddOptions(options);
        }
        
        private void Awake()
        {
            trainDropdown = GetComponent<TMP_Dropdown>();
        }
        
        private void OnEnable()
        {
            ServiceManager.I.OnServiceListUpdated += RefreshUI;
        }

        private void Start()
        {

            RefreshUI();
        }

        private void OnDisable()
        {
            ServiceManager.I.OnServiceListUpdated -= RefreshUI;
        }
    }
}