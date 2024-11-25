using UnityEngine;

public class Train : MonoBehaviour
{
    public string trainName;
    public float speed; // In units per second
    public int passengerCapacity;
    public int cargoCapacity;

    private Vector3 targetPosition;
    private bool isMoving;

    public void MoveTo(Vector3 destination)
    {
        targetPosition = destination;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position == targetPosition)
                isMoving = false;
        }
    }

    public void LoadPassengers(int amount)
    {
        // Logic for loading passengers
    }

    public void UnloadPassengers()
    {
        // Logic for unloading passengers
    }
}