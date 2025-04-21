using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public float acceleration = 0.1f; // Speed increase over time
    public float maxSpeed = 5f; // Maximum speed limit
    private float currentSpeed = 0f;

    void Update()
    {
        // Increase speed over time
        currentSpeed += acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        // Move the train forward on the X-axis
        transform.position += Vector3.right * currentSpeed * Time.deltaTime;
    }
}
