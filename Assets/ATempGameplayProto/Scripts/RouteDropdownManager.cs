using System;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RouteDropdownManager : MonoBehaviour
{
    public List<Route> routes;

    public Route selectedRoute;
    
    [Header("Infos")]
    public TextMeshProUGUI distanceText;
    
    public void OnRouteSelected(int index)
    {
        selectedRoute = routes[index];
        UpdateInfos();
    }

    private void UpdateInfos()
    {
        distanceText.text = $"<i>Distance: {selectedRoute.distance:F1} km</i>";
    }

    private void Start()
    {
        selectedRoute = routes[0];
        UpdateInfos();
    }
}
