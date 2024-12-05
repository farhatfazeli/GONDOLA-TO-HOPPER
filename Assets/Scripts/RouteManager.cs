using System.Collections.Generic;
using System.Linq;
using CustomEventSystem;
using ScriptableObjects;
using Train;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public Route activeRoute;

    public List<Route> routes;

    private LandscapeManager _landscapeManager;

    private TrainController _trainController;

    public CustomEvent departure;
    public CustomEvent arrival;

    private bool _hasDeparted;
    private bool _hasArrived;

    public void InitializeRoute(Route route)
    {
        activeRoute = route;
        if (_landscapeManager != null) Destroy(_landscapeManager);
        _landscapeManager = gameObject.AddComponent<LandscapeManager>().Initialize(activeRoute);
    }

    public List<string> GetRouteNames()
    {
        return routes.Select(route => route.routeName).ToList();
    }

    private void Awake()
    {
        _trainController = FindFirstObjectByType<TrainController>();
    }

    private void Update()
    {
        if (_trainController.transform.position.x > 460)
        {
            if (_hasArrived) return;
            arrival.Raise();
            _hasArrived = true;
        }

        if (_trainController.transform.position.x is > 50 and < 460)
        {
            if (_hasDeparted) return;
            departure.Raise();
            _hasDeparted = true;
        }
    }
}