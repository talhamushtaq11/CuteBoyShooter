using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> platformPrefabs; // List of different platform prefabs
    public int poolSize = 10; // Number of platforms to pool
    public float spawnRate = 2f; // Time in seconds between spawns
    public float minX = -5f; // Minimum X position offset
    public float maxX = 5f; // Maximum X position offset
    public float minY = -2f; // Minimum Y position offset
    public float maxY = 2f; // Maximum Y position offset
    public float minDistance = 1f; // Minimum distance between platforms

    private Queue<GameObject> platformPool = new Queue<GameObject>();
    private Vector2 lastPlatformPosition;

    private void Start()
    {
        // Initialize the pool with inactive platforms
        for (int i = 0; i < poolSize; i++)
        {
            GameObject platform = Instantiate(GetRandomPlatformPrefab());
            platform.SetActive(false);
            platformPool.Enqueue(platform);
        }

        // Set the starting position for platforms
        lastPlatformPosition = transform.position;

        // Start spawning platforms
        InvokeRepeating(nameof(SpawnPlatform), 0f, spawnRate);
    }

    private void SpawnPlatform()
    {
        GameObject platform = GetPlatform();
        float platformWidth = platform.GetComponent<Collider2D>().bounds.size.x;

        Vector2 spawnPosition = GetRandomPosition();
        int maxAttempts = 100;
        int attempts = 0;

        // Ensure new platform does not overlap with the last one
        while (Vector2.Distance(spawnPosition, lastPlatformPosition) < minDistance + platformWidth / 2f && attempts < maxAttempts)
        {
            spawnPosition = GetRandomPosition();
            attempts++;
        }

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Failed to find a valid spawn position.");
        }

        platform.transform.position = spawnPosition;
        platform.SetActive(true);
        lastPlatformPosition = spawnPosition;
    }

    private GameObject GetPlatform()
    {
        if (platformPool.Count > 0)
        {
            GameObject platform = platformPool.Dequeue();
            return platform;
        }
        else
        {
            // If no platforms are available, create a new one
            return Instantiate(GetRandomPlatformPrefab());
        }
    }

    private GameObject GetRandomPlatformPrefab()
    {
        if (platformPrefabs.Count == 0)
        {
            Debug.LogError("Platform prefabs list is empty.");
            return null;
        }

        // Randomly select a platform prefab from the list
        return platformPrefabs[Random.Range(0, platformPrefabs.Count)];
    }

    public void ReturnPlatform(GameObject platform)
    {
        platform.SetActive(false);
        platformPool.Enqueue(platform);
    }

    private Vector2 GetRandomPosition()
    {
        // Calculate a position relative to the last platform position
        float xOffset = Random.Range(minX, maxX);
        float yOffset = Random.Range(minY, maxY);
        return new Vector2(lastPlatformPosition.x + xOffset, lastPlatformPosition.y + yOffset);
    }
}
