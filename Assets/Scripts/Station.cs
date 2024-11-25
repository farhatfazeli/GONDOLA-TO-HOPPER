using UnityEngine;

public class Station : MonoBehaviour
{
    public string stationName;
    public int passengerGenerationRate;
    public int cargoGenerationRate;

    private int storedPassengers;
    private int storedCargo;

    private void Update()
    {
        // Generate resources over time
        storedPassengers += (int)(passengerGenerationRate * Time.deltaTime);
        storedCargo += (int)(cargoGenerationRate * Time.deltaTime);
    }

    public void LoadTrain(Train train)
    {
        int passengersToLoad = Mathf.Min(train.passengerCapacity, storedPassengers);
        storedPassengers -= passengersToLoad;
        train.LoadPassengers(passengersToLoad);

        // Similar logic for cargo
    }
}