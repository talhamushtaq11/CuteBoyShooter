using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformPool platformPool;

    private void OnBecameInvisible()
    {
        // Return the platform to the pool when it goes off-screen
        platformPool.ReturnPlatform(gameObject);
    }
}
