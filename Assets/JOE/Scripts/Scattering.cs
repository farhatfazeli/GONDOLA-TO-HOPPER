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

    private void Awake()
    {
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
        if (parallaxLayer.id == 0)
        {
            SpawnAssetsOnLayer();
        }
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
            float prefabHeight = prefab.GetComponent<SpriteRenderer>().bounds.size.y;
            float prefabWidth = prefab.GetComponent<SpriteRenderer>().bounds.size.x;

            float spawnX = lastSpawnPosX + prefabWidth * spawnPaddingFactor;

            float spawnY = Random.Range(bounds.min.y + offsetY, bounds.max.y - offsetY);
            spawnY = Mathf.Clamp(spawnY, bounds.min.y, bounds.max.y);

            Vector2 spawnPos = new Vector2(spawnX, spawnY);
            GameObject spawnedObject = Instantiate(prefab, spawnPos, Quaternion.identity);
            scatteredObjects.Add(spawnedObject);

            float randomScaleFactor = Random.Range(minScaleFactor, maxScaleFactor);
            spawnedObject.transform.localScale *= randomScaleFactor;

            SpriteRenderer objRenderer = spawnedObject.GetComponent<SpriteRenderer>();
            objRenderer.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;

            lastSpawnPosX = spawnX + prefabWidth;
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
