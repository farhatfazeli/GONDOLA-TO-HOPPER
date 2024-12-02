using UnityEngine;

public static class FunctionLibrary
{
    public static float TractionCalculator(float speed, float maxSpeed, float coefficient)
    {
        float normalisedSpeed = speed / maxSpeed;

        return normalisedSpeed switch
        {
            < 0 => 0f,
            < 0.316f => coefficient,
            < 1f => coefficient * (1.5f * Mathf.Pow((normalisedSpeed - 1f), 2f) + 0.3f),
            _ => coefficient * 0.3f
        };
    }
}
