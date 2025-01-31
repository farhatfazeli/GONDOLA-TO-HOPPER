using System.Collections.Generic;
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

    private void Awake()
    {
        Bounds[] bounds = new Bounds[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            bounds[i] = layers[i].GetComponent<SpriteRenderer>().bounds;
        }

        foreach (var scatterObj in objectsToScatter)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                ScatterOnLayer(scatterObj.prefabs, scatterObj.amount, bounds[i], layers[i]);
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

            GameObject spawnedObject = Instantiate(prefab, spawnPos, Quaternion.identity, layer);

            SpriteRenderer objRenderer = spawnedObject.GetComponent<SpriteRenderer>();
            objRenderer.sortingOrder = layer.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }

}