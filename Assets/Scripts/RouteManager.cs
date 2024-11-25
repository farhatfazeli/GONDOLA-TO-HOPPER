using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public Route activeRoute;

    public List<Route> routes;
    
    private LandscapeManager _landscapeManager;
    
    public void InitializeRoute(Route route)
    {
        activeRoute = route;
        if(_landscapeManager != null)
        {
            Destroy(_landscapeManager);
        }
        _landscapeManager = gameObject.AddComponent<LandscapeManager>().Initialize(activeRoute);
    }
    
    public List<string> GetRouteNames()
    {
        return routes.Select(route => route.routeName).ToList();
    }
}