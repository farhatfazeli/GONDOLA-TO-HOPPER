using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIRouteSelector : MonoBehaviour
    {
        public TMP_Dropdown dropdown;
        public RouteManager routeManager;

        private void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();
            routeManager = FindAnyObjectByType<RouteManager>();
            
            InitializeDropDown();
            
            dropdown.onValueChanged.AddListener(OnRouteSelect);
        }

        private void InitializeDropDown()
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(routeManager.GetRouteNames());
        }

        public void OnRouteSelect(int index)
        {
            if (index < 0 || index >= routeManager.routes.Count)
            {
                Debug.LogError("Invalid dropdown selection index.");
                return;
            }

            routeManager.LoadRoute(routeManager.routes[index]);
        }
    }
}