using ScriptableObjects;
using Train.Model;
using Trains.Model;
using UnityEngine;

public class TempTrainModel
{
    private readonly TrainEngine _trainEngine;
    public TempTrainModel(TrainConsistModel _)
    {
        _trainEngine = new TrainEngine(_.maxSpeed, _.tractionCoefficient, _.brakingCoefficient, _.totalMass);
    }

    public void DispatchTrain()
    {
        _trainEngine.Start();
    }
    
    public float Update(float deltaTime)
    {
        _trainEngine.Update(deltaTime);
        return _trainEngine.Position;
    }
}
