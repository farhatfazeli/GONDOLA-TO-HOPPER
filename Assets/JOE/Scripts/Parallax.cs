using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] GameObject cam;
    private float length, startPos;
    [SerializeField] float parallaxFactor;
    [SerializeField] public bool isLooping;
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxFactor);
        float distance = cam.transform.position.x * parallaxFactor;
        Vector3 newPos = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        transform.position = newPos;

        if (temp > startPos + (length / 2))
        {
            isLooping = true;
            startPos += length;
            Invoke(nameof(ResetLoopCheck), 0.1f);
        }

    }

    private void ResetLoopCheck()
    {
        isLooping = false;
    }
}


