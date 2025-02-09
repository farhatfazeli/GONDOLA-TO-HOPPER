using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] GameObject cam;
    private float length, startPos;
    [SerializeField] float parallaxFactor;
    public static event System.Action<Parallax> OnLooped;

    public int id; 

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxFactor);
        float distance = cam.transform.position.x * parallaxFactor;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + (length / 2))
        {
            startPos += length;
            OnLooped?.Invoke(this); 
        }
    }
}