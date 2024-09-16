using UnityEngine;
using System.Collections.Generic;

public class PlatformPool : MonoBehaviour
{
    public GameObject platformPrefab; // The platform prefab
    public int initialPoolSize = 10; // Initial number of platforms in the pool

    private Queue<GameObject> platformQueue = new Queue<GameObject>();

    private void Start()
    {
        // Initialize the pool with inactive platforms
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject platform = Instantiate(platformPrefab);
            platform.SetActive(false);
            platformQueue.Enqueue(platform);
        }
    }

    // Get a platform from the pool
    public GameObject GetPlatform()
    {
        if (platformQueue.Count > 0)
        {
            GameObject platform = platformQueue.Dequeue();
            platform.SetActive(true);
            return platform;
        }
        else
        {
            // If no platforms are available, create a new one
            GameObject platform = Instantiate(platformPrefab);
            return platform;
        }
    }

    // Return a platform to the pool
    public void ReturnPlatform(GameObject platform)
    {
        platform.SetActive(false);
        platformQueue.Enqueue(platform);
    }
}
