public class Route
{
    public Station startStation;
    public Station endStation;
    public float distance; // In Unity units

    public float GetTravelTime(float trainSpeed)
    {
        return distance / trainSpeed;
    }
}