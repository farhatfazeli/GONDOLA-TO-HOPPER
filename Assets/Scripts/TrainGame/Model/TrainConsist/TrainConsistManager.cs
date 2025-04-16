using System;
using System.Collections.Generic;
using System.Linq;
using TrainGame.Model.RollingStock;
using UnityEngine;

namespace TrainGame.Model.TrainConsist
{
    public class TrainConsistManager
    {
        private readonly TrainConsistRepository _repository;

        public readonly TrainConsistQueryService QueryService;
        public IReadOnlyCollection<TrainConsistModel> AllTrainConsists => _repository.List;
        
        public event Action OnTrainConsistListUpdated
        {
            add => _repository.ListUpdated += value;
            remove => _repository.ListUpdated -= value;
        }

        public bool CreateTrainConsist(string trainName, List<RollingStockModel> rollingStockModels)
        {
            if(rollingStockModels == null)
                throw new ArgumentNullException(nameof(rollingStockModels));
            
            if(!rollingStockModels.Any())
                throw new InvalidOperationException("rollingStockModels contains no elements");
            
            TrainConsistModelFactory trainConsistModelFactory = new TrainConsistModelFactory();
            TrainConsistModel trainConsistModel = trainConsistModelFactory.CreateTrainConsist(trainName, rollingStockModels);
            
            _repository.Add(trainConsistModel);

            foreach (TrainConsistModel tcm in _repository.List.ToList())
            {
                Debug.Log(tcm.name);
            }
            
            return true;
        }

        public void LoadTrainConsists(List<TrainConsistModel> trainConsistModels)
        {
            _repository.AddRange(trainConsistModels);
        }

        public void Clear()
        {
            _repository.Clear();
        }
        
        private static TrainConsistManager instance;
        public static TrainConsistManager I => instance ??= new TrainConsistManager();
        private TrainConsistManager()
        {
            _repository = new TrainConsistRepository();
            QueryService = new TrainConsistQueryService(_repository);
        }
    }
}