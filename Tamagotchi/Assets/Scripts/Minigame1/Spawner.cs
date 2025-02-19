// Spawner.cs
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjectPooler objectPooler; // Reference to the Object Pooler
    public float spawnRate = 2f; // Time between obstacle spawns
    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            SpawnObstacle(); // Only spawn obstacles
            spawnTimer = 0f; // Reset the timer
        }
    }

    void SpawnObstacle()
    {
        // Choose the obstacle type (for example, let's randomly choose between type 1 and type 2)
        int obstacleType = Random.Range(0, 2) == 0 ? 1 : 2;

        // Get an obstacle from the pool (pass the obstacle type)
        GameObject obstacle = objectPooler.GetObstacleFromPool(obstacleType);

        // Set the spawn position (randomized for demo purposes)
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 7f, 0);
        obstacle.transform.position = spawnPosition;

        // Set the speed for the obstacle
        ObstacleMovement obstacleMovement = obstacle.GetComponent<ObstacleMovement>();
        if (obstacleMovement != null)
        {
            obstacleMovement.SetSpeed(1f); // Example speed
        }
    }

    // Call this function to stop obstacle spawning when the game is over
    public void StopSpawning()
    {
        // You can stop obstacle spawning here if needed
        spawnRate = 0f; // Set spawn rate to 0 to prevent further spawns
    }
}
