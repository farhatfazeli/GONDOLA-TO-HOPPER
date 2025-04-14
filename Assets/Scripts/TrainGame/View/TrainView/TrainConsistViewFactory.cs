using System.Collections.Generic;
using TrainGame.Model;
using TrainGame.Model.RollingStock;
using TrainGame.Model.TrainConsist;
using UnityEngine;

namespace TrainGame.View.TrainView
{
    public class TrainConsistViewFactory : MonoBehaviour
    {
        public TrainConsistView CreateTrainConsistView(TrainConsistModel trainConsistModel, Transform spawnTransform)
        {
            TrainConsistView trainConsistView = new GameObject(trainConsistModel.name).
                AddComponent<TrainConsistView>().
                Construct(trainConsistModel);

            trainConsistView.transform.position = spawnTransform.position;
            
            CreateRollingStock(trainConsistView, trainConsistModel.RollingStock);
            
            return trainConsistView;
        }

        private void CreateRollingStock(TrainConsistView trainConsistView, List<RollingStockModel> rollingStock)
        {
            foreach (var rollingStockModel in rollingStock)
            {
                InstantiateRollingStock(trainConsistView, rollingStockModel);
            }
        }
        
        private void InstantiateRollingStock(TrainConsistView trainConsistView, RollingStockModel rollingStockModel)
        {
            GameObject rollingStockPrefab = RollingStockRepository.I.GetViewPrefab(rollingStockModel);
            
            GameObject rollingStockGo = Instantiate(rollingStockPrefab, Vector3.zero, Quaternion.identity, trainConsistView.transform);
            rollingStockGo.name = rollingStockModel.name;
            
            RollingStockView rollingStockView = rollingStockGo.GetComponent<RollingStockView>().
                Initialize(rollingStockModel)
                .AlignRollingStock(GetInstantiatePosition(trainConsistView));
            trainConsistView.PopulateRollingStockViews(rollingStockView);
        }

        private Transform GetInstantiatePosition(TrainConsistView trainConsistView)
        {
            if (trainConsistView.IsTrainConsistEmpty())
            {
                return trainConsistView.transform;
            }
            else
            {
                Transform rearOfTrainConsist = trainConsistView.GetRearOfTrainConsist();
                return rearOfTrainConsist;
            }
        }
    }
}