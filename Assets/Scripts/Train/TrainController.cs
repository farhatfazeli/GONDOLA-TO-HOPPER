using System.Collections.Generic;
using ScriptableObjects;
using Train.Model;
using Train.View;
using UnityEngine;

namespace Train
{
    public class TrainController : MonoBehaviour
    {
        private readonly List<TrainModel> _trainModels = new();

        public TrainModel trainModel;

        public Locomotive testLocomotive;

        public float lolfloat;

        private void Start()
        {
            CreateTrainModel(testLocomotive);
        }

        private void Update()
        {
            foreach (var model in _trainModels)
                model.Update(Time.deltaTime);
        }

        private TrainModel CreateTrainModel(Locomotive locomotive)
        {
            trainModel = new TrainModel(locomotive);
            _trainModels.Add(trainModel);
            var trainView = FindFirstObjectByType<TrainView>();
            trainView.trainModel = trainModel;
            return trainModel;
        }
        
        public void DispatchTrain()
        {
            trainModel.StartEngine();
            Debug.Log("Train dispfatched");
        }

        public void ShowView()
        {
            Debug.Log("Train view shown");
        }

        public void DestroyView()
        {
            Debug.Log("Train view destroyed");
        }
    }
}