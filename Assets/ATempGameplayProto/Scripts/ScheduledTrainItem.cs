using ScriptableObjects;
using UnityEngine;

public enum LoadType
{
    Passengers,
    Cargo
}

public class Progress
{
    public float progress;
}

public class ScheduledTrainItem
{
    public Route route;
    public TempTrain tempTrain;
    public LoadType loadType;
    public float loadAmount;
    public Progress loadProgress;
    public Progress travelProgress;
    public Progress unloadProgress;
    public bool isComplete;
}
