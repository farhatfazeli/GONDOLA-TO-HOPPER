using ScriptableObjects;
using Train.Model;
using UnityEngine;

public class TempTrainModel
{
    private readonly TrainEngine _trainEngine;
    public TempTrainModel(TempTrain _)
    {
        _trainEngine = new TrainEngine(_.maxSpeed, _.tractionCoefficient, _.brakingCoefficient, _.mass);
    }

    public void DispatchTrain(Route route)
    {
        _trainEngine.Start();
    }
    
    public void Update(float deltaTime)
    {
        _trainEngine.Update(deltaTime);
    }
}
