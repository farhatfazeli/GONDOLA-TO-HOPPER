using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text moneyText;
    public Text passengerText;
    public Text cargoText;

    private void Update()
    {
        moneyText.text = "Money: " + GameManager.Instance.money;
        passengerText.text = "Passengers: " + GameManager.Instance.passengers;
        cargoText.text = "Cargo: " + GameManager.Instance.cargo;
    }

    public void OnUpgradeTrainButtonClicked()
    {
        // Logic for upgrading the selected train
    }
}