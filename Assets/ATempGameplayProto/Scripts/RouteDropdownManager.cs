using System;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RouteDropdownManager : MonoBehaviour
{
    public List<SO_Route> routes;

    public SO_Route selectedSoRoute;
    
    [Header("Infos")]
    public TextMeshProUGUI distanceText;
    
    public void OnRouteSelected(int index)
    {
        selectedSoRoute = routes[index];
        UpdateInfos();
    }

    private void UpdateInfos()
    {
        distanceText.text = $"<i>Distance: {selectedSoRoute.distance:F1} km</i>";
    }

    private void Start()
    {
        selectedSoRoute = routes[0];
        UpdateInfos();
    }
}
