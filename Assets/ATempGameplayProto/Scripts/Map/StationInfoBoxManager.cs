using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StationInfoBoxManager : MonoBehaviour, IInfoBoxHoverHandler
{
    public Station station;
    
    [Header("UI Elements")]
    public TextMeshProUGUI stationName;
    public TextMeshProUGUI stationResourceCost;
    public TextMeshProUGUI routeResourceCost;
    public TextMeshProUGUI distanceText;
    public Button buildStationButton;
    public Button buildRouteButton;
    
    public Slider roadBuildProgressSlider;
    
    private Image _image;
    private void Start()
    {
        stationName.text = station.stationName;
        stationResourceCost.text = $"Station cost: {station.resourceCost:N0} tonne-passengers";
        routeResourceCost.text = $"Route cost: {station.route.resourceCost:N0} tonne-kms";
        distanceText.text = $"Distance: {station.route.distance:N0} km";
        
        buildStationButton.onClick.AddListener(BuildStation);
        buildRouteButton.onClick.AddListener(BuildRoute);
        
        _image = GetComponent<Image>();
    }

    private void BuildStation()
    {
        FinishBuildStation();
    }

    private void FinishBuildStation()
    {
        stationResourceCost.gameObject.SetActive(false);
        buildStationButton.gameObject.SetActive(false);
        buildRouteButton.interactable = true;
    }
    
    private void BuildRoute()
    {
        FinishBuildRoute();
    }

    private void FinishBuildRoute()
    {
        _image.color = new Color32(0xF6, 0xFF, 0xAA, 0xFF);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
