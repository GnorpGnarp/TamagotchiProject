using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstaclePrefab1; // First obstacle
    public GameObject obstaclePrefab2; // Second obstacle
    public GameObject coinPrefab;
    public float initialSpawnRate = 2f; // Time between spawns at the start
    private float spawnRate;
    private float timePassed;
    private bool gameIsOver = false;

    public Transform[] spawnPoints; // Array of spawn points for random selection
    public Transform ySpawnLimit; // Reference to an empty object to limit Y position

    public float initialObstacleSpeed = 1f; // Speed at which obstacles move up
    private float obstacleSpeed; // The speed at which obstacles move, which increases over time

    void Start()
    {
        spawnRate = initialSpawnRate;
        obstacleSpeed = initialObstacleSpeed;
        StartCoroutine(SpawnObstacleAndCoin());
    }

    void Update()
    {
        if (!gameIsOver)
        {
            // Gradually decrease spawnRate and increase obstacle speed over time
            timePassed += Time.deltaTime;
            if (timePassed >= 30f) // Every 30 seconds, make things faster
            {
                spawnRate -= 0.1f; // Decrease spawn time by 0.1 seconds
                obstacleSpeed += 0.1f; // Increase the obstacle speed
                timePassed = 0f;   // Reset the time counter
            }
        }
    }

    // Coroutine for spawning obstacles and coins
    private IEnumerator SpawnObstacleAndCoin()
    {
        while (!gameIsOver)
        {
            // Randomly select a spawn point for X-axis (left or right)
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Randomly select between two obstacle types
            GameObject selectedObstacle = Random.Range(0f, 1f) > 0.5f ? obstaclePrefab1 : obstaclePrefab2;

            // Set the Y position to the position of the ySpawnLimit (the empty object)
            Vector3 spawnPosition = new Vector3(spawnPoint.position.x, ySpawnLimit.position.y, spawnPoint.position.z);

            // Instantiate selected obstacle and coin at the new position
            GameObject obstacle = Instantiate(selectedObstacle, spawnPosition, Quaternion.identity);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);

            // Set the obstacle's movement speed
            ObstacleMovement obstacleMovement = obstacle.GetComponent<ObstacleMovement>();
            if (obstacleMovement != null)
            {
                obstacleMovement.SetSpeed(obstacleSpeed);
            }

            // Wait before spawning next set of objects
            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void StopSpawning()
    {
        gameIsOver = true;
    }
}
