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
    [SerializeField] float offsetX, offsetY;
    private List<GameObject> scatteredObjects = new List<GameObject>();

    [SerializeField] Camera cam;
    private float screenRight;

    private float lastSpawnPosX;

    [SerializeField] float spawnPaddingFactor = 1.5f;

    [SerializeField] float minScaleFactor = 0.5f;
    [SerializeField] float maxScaleFactor = 1.5f;

    SpriteRenderer layerSR;

    private float length;
    private float startPos;

    private void Awake()
    {
        layerSR = GetComponent<SpriteRenderer>();
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;  
        UpdateScreenLimits();
        Parallax.OnLooped += HandleParallaxLoop;
    }

    private void OnDestroy()
    {
        Parallax.OnLooped -= HandleParallaxLoop;
    }

    private void Update()
    {
        UpdateScreenLimits();
        RemoveOffscreenFoliage();
    }

    private void HandleParallaxLoop(Parallax parallaxLayer)
    {
        SpawnAssetsOnLayer();
    }

    private void SpawnAssetsOnLayer()
    {
        Bounds bounds = GetComponent<SpriteRenderer>().bounds;

        if (lastSpawnPosX == 0)
        {
            lastSpawnPosX = screenRight;
        }

        foreach (var scatterObj in objectsToScatter)
        {
            ScatterObjects(scatterObj.prefabs, scatterObj.amount, bounds);
        }
    }

    private void ScatterObjects(GameObject[] prefabs, int amount, Bounds bounds)
    {
        if (prefabs.Length == 0) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            float prefabWidth = prefab.GetComponent<SpriteRenderer>().bounds.size.x;
            float prefabHeight = prefab.GetComponent<SpriteRenderer>().bounds.size.y;

            float spawnX = lastSpawnPosX + prefabWidth * spawnPaddingFactor;
            float spawnY = Random.Range(bounds.min.y + offsetY, bounds.max.y - offsetY);

            Vector2 spawnPos = new Vector2(spawnX, spawnY + (prefabHeight/2));
            GameObject spawnedObject = Instantiate(prefab, spawnPos, Quaternion.identity);

            SpriteRenderer objRenderer = spawnedObject.GetComponent<SpriteRenderer>();
            if (objRenderer != null)
            {
                objRenderer.sortingLayerID = layerSR.sortingLayerID;
                objRenderer.sortingOrder = layerSR.sortingOrder+1;
            }

            scatteredObjects.Add(spawnedObject);

            lastSpawnPosX = spawnX + prefabWidth * spawnPaddingFactor;
        }
    }
    private void UpdateScreenLimits()
    {
        Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
        screenRight = rightEdge.x;
    }

    private void RemoveOffscreenFoliage()
    {
        for (int i = scatteredObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = scatteredObjects[i];
            float objX = obj.transform.position.x;
            float spriteWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;

            if (objX + spriteWidth < cam.ViewportToWorldPoint(Vector3.zero).x)
            {
                scatteredObjects.RemoveAt(i);
                Destroy(obj);
            }
        }
    }
}
