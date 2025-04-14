using System.Collections.Generic;
using TMPro;
using TrainGame.Model.TrainConsist;
using UnityEngine;

namespace TrainGame.View.UIView
{
    public class PlannerTrainSelectionView : MonoBehaviour
    {
        [Header("UI Elements")] 
        public TMP_Dropdown trainDropdown;

        private TrainConsistRepository _trainConsistRepository;
        
        private void RefreshUI()
        {
            List<TrainConsistModel> trains = _trainConsistRepository.GetStandbyTrainConsists();
            List<string> options = trains.ConvertAll(train => train.name);
            trainDropdown.ClearOptions();
            trainDropdown.AddOptions(options);
        }
        
        private void Awake()
        {
            trainDropdown = GetComponent<TMP_Dropdown>();
            _trainConsistRepository = TrainConsistRepository.I;
        }
        
        private void OnEnable()
        {
            _trainConsistRepository.OnTrainListUpdated += RefreshUI;
        }

        private void Start()
        {

            RefreshUI();
        }

        private void OnDisable()
        {
            _trainConsistRepository.OnTrainListUpdated -= RefreshUI;
        }
    }
}