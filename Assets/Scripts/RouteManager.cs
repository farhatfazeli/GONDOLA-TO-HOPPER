using System.Collections.Generic;
using System.Linq;
using CustomEventSystem;
using ScriptableObjects;
using Trains;
using Trains.View;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public Route activeRoute;

    public List<Route> routes;

    private LandscapeManager _landscapeManager;

    private TrainView _trainView;

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
        _trainView = FindFirstObjectByType<TrainView>();
    }

    private void Update()
    {
        if (_trainView.transform.position.x > 380)
        {
            if (_hasArrived) return;
            arrival.Raise();
            _hasArrived = true;
        }

        if (_trainView.transform.position.x is > 40 and < 380)
        {
            if (_hasDeparted) return;
            departure.Raise();
            _hasDeparted = true;
        }
    }
}