using System.Collections.Generic;
using ScriptableObjects;
using UI;
using UnityEngine;

public class LandscapeManager : MonoBehaviour
{
    private IEnumerator<Landscape> _landscapes;
    
    private UILandscapePanelHandler _uiLandscapePanelHandler;
    
    public LandscapeManager Initialize(Route route)
    {
        _landscapes = route.landscapes.GetEnumerator();
        _uiLandscapePanelHandler = FindAnyObjectByType<UILandscapePanelHandler>();
        TurnPage();
        return this;
    }

    public void TurnPage()
    {
        if (_landscapes.MoveNext()) // Move to the next landscape
        {
            _uiLandscapePanelHandler.LoadLandscape(_landscapes.Current);
        }
        else
        {
            Debug.Log("Reached the end of the landscapes.");
        }
    }
}
