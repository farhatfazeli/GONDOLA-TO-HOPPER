using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchInfoBoxManager : MonoBehaviour, IInfoBoxHoverHandler
{
    public RollingStock researchSubject;
    
    [Header("UI Elements")]
    public TextMeshProUGUI researchName;
    public TextMeshProUGUI researchDescription;
    public TextMeshProUGUI researchCost;
    public Button researchButton;
    
    private void Start()
    {
        researchName.text = researchSubject.Name;
        researchDescription.text = GetResearchDescription();
        researchCost.text = $"Cost: {researchSubject.UnlockCost:N0} RP";
    }
    
    private string GetResearchDescription()
    {
        string cargoType = "";
        string wagonType = "Locomotive";
        if (researchSubject.Type == RollingStockType.Wagon)
        {
            if((researchSubject as Wagon).cargoType == CargoType.Passengers)
            {
                cargoType = "Passenger ";
            }
            else
            {
                cargoType = "Freight ";
            }
            wagonType = "Wagon";
        }
        return $"{cargoType}{wagonType}";
    }

    private void Research()
    {
        FinishResearch();
    }

    private void FinishResearch()
    {
        researchCost.gameObject.SetActive(false);
        researchButton.gameObject.SetActive(false);
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

public enum AchievementState
{
    Achieved,
    Available,
    Unavailable
}