using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

public class Scattering : MonoBehaviour
{
    [System.Serializable]
    public struct ScatterObject
    {
        public GameObject[] prefabs;
        public int amount;
    }

    [SerializeField] ScatterObject[] objectsToScatter;
    [SerializeField] Transform[] layers;
    [SerializeField] float overlapOffset, offsetX, offsetY;
    private List<Vector2> usedPositions = new List<Vector2>();  //to-do
    private List<GameObject> scatteredObjects = new List<GameObject>();
    private Bounds[] bounds;

    [SerializeField] Camera cam;
    private float camHeight, camWidth, leftBound, rightBound;

    [SerializeField] Parallax[] parallaxSystem;
    private void Awake()
    {
        camHeight = cam.orthographicSize * 2f;
        camWidth = camHeight * cam.aspect;
        leftBound = cam.transform.position.x - (camWidth/ 2);
        rightBound = cam.transform.position.x + (camWidth/ 2);

        bounds = new Bounds[layers.Length];
        parallaxSystem = new Parallax[layers.Length];

        for (int i = 0; i < layers.Length; i++)
        {
            bounds[i] = layers[i].GetComponent<SpriteRenderer>().bounds;
            parallaxSystem[i] = layers[i].GetComponent<Parallax>();
        }

    }
    private void Update()
    {
       // SpawnAssets();
        removeFoliage();

    }

    public void SpawnAssets()
    {
        foreach (var scatterObj in objectsToScatter)
        {
            if (parallaxSystem[parallaxSystem.Length - 1].isLooping == true)
            {
                for (int i = 0; i < layers.Length; i++)
                {
                    ScatterOnLayer(scatterObj.prefabs, scatterObj.amount, bounds[i], layers[i]);
                }
            }

        }
    }
    private void ScatterOnLayer(GameObject[] prefabs, int amount, Bounds bounds, Transform layer)
    {
        if (prefabs.Length == 0) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            float prefabHeight = prefab.GetComponent<SpriteRenderer>().bounds.size.y;
            float prefabWidth = prefab.GetComponent<SpriteRenderer>().bounds.size.x;

            float randomY = Random.Range(bounds.min.y + offsetY, bounds.max.y - offsetY);  
            float randomX = Random.Range(bounds.min.x + offsetX, bounds.max.x - offsetX);  

            Vector2 spawnPos = new Vector2(randomX + prefabWidth, randomY + prefabHeight / 2);

            if (usedPositions.Contains(spawnPos))
            {
                i--; 
                continue;
            }

            usedPositions.Add(spawnPos);

            Vector2 scatterPos = new Vector2(spawnPos.x + layer.GetComponent<SpriteRenderer>().size.x, spawnPos.y);
            GameObject spawnedObject = Instantiate(prefab, scatterPos, Quaternion.identity);
            
            scatteredObjects.Add(spawnedObject);

            SpriteRenderer objRenderer = spawnedObject.GetComponent<SpriteRenderer>();
            objRenderer.sortingOrder = layer.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }

    private void removeFoliage()
    {
        //write logic to remove foliage that passed
        for (int i = 0; i < scatteredObjects.Count; i++)
        {
            GameObject scObj = scatteredObjects[i];
            float spriteWidth = scObj.GetComponent<SpriteRenderer>().bounds.size.x;
            float xPos = scObj.transform.position.x;

            if ((xPos + spriteWidth / 2) < leftBound)
            {
                scatteredObjects.RemoveAt(i);
                Destroy(scObj); 

                i--;  
            }
        }
    }

}