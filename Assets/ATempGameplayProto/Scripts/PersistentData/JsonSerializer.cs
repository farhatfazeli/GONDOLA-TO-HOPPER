using UnityEngine;

public class JsonSerializer
{
    public string Serialize()
    {
        return JsonUtility.ToJson(this, true);
    }
    
    public T Deserialize<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
