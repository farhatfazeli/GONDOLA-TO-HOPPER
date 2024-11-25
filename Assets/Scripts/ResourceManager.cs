using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int totalPassengers;
    public int totalCargo;

    public void AddPassengers(int amount)
    {
        totalPassengers += amount;
    }

    public void DeductPassengers(int amount)
    {
        totalPassengers -= Mathf.Min(amount, totalPassengers);
    }

    public void AddCargo(int amount)
    {
        totalCargo += amount;
    }

    public void DeductCargo(int amount)
    {
        totalCargo -= Mathf.Min(amount, totalCargo);
    }
}