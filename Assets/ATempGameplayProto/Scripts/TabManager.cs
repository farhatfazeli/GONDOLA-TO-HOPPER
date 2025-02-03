using System;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject plannerPanel;
    public GameObject mapPanel;
    public GameObject depotPanel;
    public GameObject researchPanel;
    
    private GameObject activePanel;

    private void Start()
    {
        plannerPanel.SetActive(false);
        mapPanel.SetActive(false);
        depotPanel.SetActive(false);    
        researchPanel.SetActive(false);
        // Initialize by showing the Planner panel
        SwitchToPanel(plannerPanel);
    }

    public void SwitchToPlanner()
    {
        SwitchToPanel(plannerPanel);
        plannerPanel.GetComponentInChildren<SchedulerManager>().RefreshUI();
    }

    public void SwitchToMap()
    {
        SwitchToPanel(mapPanel);
    }

    public void SwitchToDepot()
    {
        SwitchToPanel(depotPanel);
    }

    public void SwitchToResearch()
    {
        SwitchToPanel(researchPanel);
    }

    private void SwitchToPanel(GameObject panelToActivate)
    {
        if (activePanel != null)
        {
            activePanel.SetActive(false); // Hide the current active panel
        }

        panelToActivate.SetActive(true); // Show the new panel
        activePanel = panelToActivate;  // Update the active panel reference
    }
}
