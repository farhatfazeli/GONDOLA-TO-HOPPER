using UnityEngine;
using UnityEngine.Splines; // Import Unity's Spline system

public class TrainController : MonoBehaviour
{
    public SplineContainer splineContainer; // Reference to the spline container
    public float speed = 5f; // Train speed in units per second

    private Spline spline; // Reference to the spline itself
    private float progress = 0f; // Tracks progress along the spline (0 to 1)

    void Start()
    {
        if (splineContainer == null)
        {
            Debug.LogError("Spline Container is not assigned!");
            return;
        }

        spline = splineContainer.Spline;

        TravelAlongSpline(progress);
    }

    void Update()
    {
        if (spline == null) return;

        // Move along the spline based on speed and deltaTime
        progress += speed * Time.deltaTime / spline.GetLength();

        // Loop or clamp progress (optional)
        if (progress > 1f) progress = 0f; // Loop back to start
        // progress = Mathf.Clamp01(progress); // Uncomment to stop at the end

        // Move the train along the spline
        TravelAlongSpline(progress);
    }
    
    private void TravelAlongSpline(float progress)
    {
        // Get the position on the spline based on progress
        Vector3 position = spline.EvaluatePosition(progress);

        // Calculate rotation based on spline tangent
        Vector3 tangent = spline.EvaluateTangent(progress);
        Quaternion rotation = Quaternion.LookRotation(tangent);

        // Update train's position and rotation
        transform.position = position;
        transform.rotation = rotation;
    }
}