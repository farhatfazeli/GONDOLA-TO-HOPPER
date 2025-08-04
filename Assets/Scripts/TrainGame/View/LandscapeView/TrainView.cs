using System.Linq;
using ScriptableObjects;
using TrainGame.Model.Route;
using TrainGame.Model.TrainConsist;
using UnityEngine;

namespace TrainGame.View.LandscapeView
{
    public class TrainView : MonoBehaviour
    {
        // [Header("Train data")]
        // [SerializeField] private SO_Locomotive _locomotive;
        // [SerializeField] private SO_Wagon[] _wagons;
        //
        // [Header("Route data")]
        // [SerializeField] private SO_Route _route;
        //
        // private TrainDriver _trainDriver;
        // private TrainEngine _trainEngine;
        //
        // private void Awake() {
        //     Initialize();
        // }
        //
        // private void Initialize() {
        //     RouteModel routeModel = new RouteModel(_route);
        //     _trainEngine = new TrainEngine(_locomotive.maxSpeed, _locomotive.tractionCoefficient, _locomotive.brakingCoefficient, _wagons.Sum(x => x.mass) + _locomotive.mass);
        //     _trainDriver = new TrainDriver(_trainEngine, routeModel);
        // }

        // public TrainModel trainModel;
        // private TrainWheelController _wheelController;
        //
        // private void Awake()
        // {
        //     _wheelController = GetComponentInChildren<TrainWheelController>();
        // }
        //
        // private void Update()
        // {
        //     transform.position = trainModel.TransmitCurrentPosition() * Vector3.right;
        //     _wheelController.RotateWheels(trainModel.TransmitCurrentSpeed());
        // }
        //
        // public (float, float, float, float) GetPhysicsValues()
        // {
        //     return trainModel.TransmitTrainState();
        // }
    }
}