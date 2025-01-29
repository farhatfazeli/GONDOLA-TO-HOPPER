using System;
using ScriptableObjects;
using UnityEngine;

public enum LoadType
{
    Passengers,
    Cargo
}

public class Progress
{
    public bool IsComplete => _currentAmount >= _targetAmount;
    public float Value => _currentAmount / _targetAmount;

    private readonly float _targetAmount;
    private float _currentAmount;

    public Progress(float targetAmount)
    {
        _targetAmount = (targetAmount > 0) ? targetAmount : 1;
    }
    
    public void UpdateProgress(float amount)
    {
        _currentAmount += amount;
    }
    
    public void UpdateProgressPercentage(float amount)
    {
        _currentAmount += _targetAmount * amount;
    }
}

public class ScheduledTrainItem
{
    public readonly Route route;
    public readonly TempTrain tempTrain;
    public readonly LoadType loadType;
    public readonly float loadAmount;
    public readonly Progress loadProgress;
    public readonly Progress travelProgress;
    public readonly Progress unloadProgress;
    public bool isComplete;
    
    private TempTrainModel _tempTrainModel;
    
    public ScheduledTrainItem(Route route, TempTrain tempTrain, LoadType loadType, float loadAmount)
    {
        this.route = route;
        this.tempTrain = tempTrain;
        this.loadType = loadType;
        this.loadAmount = loadAmount;
        loadProgress = new Progress(loadAmount);
        travelProgress = new Progress(route.distance);
        unloadProgress = new Progress(loadAmount);
        isComplete = false;
    }
    
    public void ProgressLoadProgress(float amount)
    {
        loadProgress.UpdateProgressPercentage(amount);
        _tempTrainModel = new TempTrainModel(tempTrain);
    }
    
    public void ProgressTravelProgress(float amount)
    {
        travelProgress.UpdateProgressPercentage(amount);
        _tempTrainModel.DispatchTrain(route);
    }
    
    public void ProgressUnloadProgress(float amount)
    {
        unloadProgress.UpdateProgressPercentage(amount);
        
        if (!unloadProgress.IsComplete) return;
        
        isComplete = true;
        switch (loadType)
        {
            case LoadType.Passengers:
                ResourceManager.PassengerKm += loadAmount;
                break;
            case LoadType.Cargo:
                ResourceManager.TonneKm += loadAmount;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Update()
    {
        _tempTrainModel.Update(Time.deltaTime);
    }
}
