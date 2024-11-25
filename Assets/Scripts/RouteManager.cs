using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public Route activeRoute;

    public List<Route> routes;

    public void LoadRoute(Route route)
    {
        activeRoute = route;
        
    }
    
    public Route GetRouteByName(string routeName)
    {
        foreach (var route in routes.Where(route => route.routeName == routeName))
            return route;
        
        Debug.LogError("Route not found: " + routeName);
        
        return null;
    }
    
    public List<string> GetRouteNames()
    {
        return routes.Select(route => route.routeName).ToList();
    }
}