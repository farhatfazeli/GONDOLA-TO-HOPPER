using UnityEngine;

public interface ISerializer
{
    public string Serialize<T>(T obj);
    public T Deserialize<T>(string json);
}

public class JsonSerializer : ISerializer
{
    public string Serialize<T>(T obj)
    {
        return JsonUtility.ToJson(obj, true);
    }
    
    public T Deserialize<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
