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
    
    private readonly TempTrainModel _tempTrainModel;

    private bool _loading;
    private bool _traveling;
    private bool _unloading;
    
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
        
        _tempTrainModel = new TempTrainModel(tempTrain);
    }
    
    public void ProgressLoadProgress(float amount)
    {
        if (!_loading && !loadProgress.IsComplete)
        {
            _loading = true;
            Load();
        }
        
        loadProgress.UpdateProgressPercentage(amount);
        
        if(loadProgress.IsComplete) _loading = false;
    }
    
    private void Load()
    {
        loadProgress.UpdateProgress(SO_GameParameters.I.loadSpeedUpFactor * 10f * Time.deltaTime);
        if(loadProgress.IsComplete) _loading = false;
    }
    
    public void ProgressTravelProgress(float amount)
    {
        if (!_traveling && !travelProgress.IsComplete)
        {
            _traveling = true;
            _tempTrainModel.DispatchTrain();
            Travel();
        }
        
        travelProgress.UpdateProgressPercentage(amount);
        
        if(travelProgress.IsComplete) _traveling = false;
    }

    private void Travel()
    {
        travelProgress.UpdateProgress(SO_GameParameters.I.travelSpeedUpFactor * _tempTrainModel.Update(Time.deltaTime));
        if (travelProgress.IsComplete) _traveling = false;
    }
    
    public void ProgressUnloadProgress(float amount)
    {
        if (!_unloading && !unloadProgress.IsComplete)
        {
            _unloading = true;
            Unload();
        }
        
        unloadProgress.UpdateProgressPercentage(amount);

        if (!unloadProgress.IsComplete) return;
        _unloading = false;
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
    
    private void Unload()
    {
        unloadProgress.UpdateProgress(SO_GameParameters.I.loadSpeedUpFactor * 10f * Time.deltaTime);
        
        if (!unloadProgress.IsComplete) return;
        _unloading = false;
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
        if(_loading) Load();
        if(_traveling) Travel();
        if(_unloading) Unload();
    }
}
