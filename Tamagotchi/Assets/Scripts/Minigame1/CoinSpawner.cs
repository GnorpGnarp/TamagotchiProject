// CoinSpawner.cs
using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public ObjectPooler objectPooler;
    public float spawnRate = 2f; // Time between coin spawns
    public Transform ySpawnLimit; // Reference to the spawn limit for Y position
    private bool gameIsOver = false; // Flag to stop spawning when the game is over

    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    private IEnumerator SpawnCoins()
    {
        while (!gameIsOver)
        {
            // Get a coin from the pool
            GameObject coin = objectPooler.GetCoinFromPool();

            // Set the spawn position for the coin
            Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), ySpawnLimit.position.y, 0f);
            coin.transform.position = spawnPosition;

            // Set the speed for the coin (you can define a coin-specific speed)
            CoinMovement coinMovement = coin.GetComponent<CoinMovement>();
            if (coinMovement != null)
            {
                coinMovement.SetSpeed(1f); // Example speed
            }

            yield return new WaitForSeconds(spawnRate); // Wait before spawning the next coin
        }
    }

    // Call this function to stop coin spawning when the game is over
    public void StopSpawning()
    {
        gameIsOver = true;
    }
}
