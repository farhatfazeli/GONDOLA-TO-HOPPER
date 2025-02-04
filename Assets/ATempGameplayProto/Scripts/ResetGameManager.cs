using ScriptableObjects;
using UnityEngine;

public class ResetGameManager : MonoBehaviour
{
    public void ResetGame()
    {
        ResetResearch();
    }

    private void ResetResearch()
    {
        Locomotive[] loadedLocomotives = Resources.LoadAll<Locomotive>(SO_GameParameters.I.locomotivesFolderPathShort);
        foreach (var locomotive in loadedLocomotives)
        {
            locomotive.Reset();
        }
        
        Wagon[] loadedWagons = Resources.LoadAll<Wagon>(SO_GameParameters.I.wagonsFolderPathShort);
        foreach (var wagon in loadedWagons)
        {
            wagon.Reset();
        }
        
        TempTrain[] loadedTrains = Resources.LoadAll<TempTrain>(SO_GameParameters.I.trainsFolderPathShort);
        foreach (var train in loadedTrains)
        {
            train.fileDeleted = true;
        }
        
    }
}
