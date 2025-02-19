using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // Prefabs to pool (Obstacles and Coin)
    public GameObject obstaclePrefab1;
    public GameObject obstaclePrefab2;
    public GameObject coinPrefab;

    // Pool size (number of objects to instantiate at start)
    public int poolSize = 10;

    // Queues to hold the objects in the pool
    private Queue<GameObject> obstaclePool1 = new Queue<GameObject>();
    private Queue<GameObject> obstaclePool2 = new Queue<GameObject>();
    private Queue<GameObject> coinPool = new Queue<GameObject>();

    void Start()
    {
        // Initialize the pools with inactive objects
        for (int i = 0; i < poolSize; i++)
        {
            // Create and deactivate obstacle 1 objects
            GameObject obstacle1 = Instantiate(obstaclePrefab1);
            obstacle1.SetActive(false);
            obstaclePool1.Enqueue(obstacle1);

            // Create and deactivate obstacle 2 objects
            GameObject obstacle2 = Instantiate(obstaclePrefab2);
            obstacle2.SetActive(false);
            obstaclePool2.Enqueue(obstacle2);

            // Create and deactivate coin objects
            GameObject coin = Instantiate(coinPrefab);
            coin.SetActive(false);
            coinPool.Enqueue(coin);
        }
    }

    // Get obstacle from pool (choose the type of obstacle)
    public GameObject GetObstacleFromPool(int obstacleType)
    {
        GameObject obstacle = null;

        if (obstacleType == 1 && obstaclePool1.Count > 0)
        {
            obstacle = obstaclePool1.Dequeue();
        }
        else if (obstacleType == 2 && obstaclePool2.Count > 0)
        {
            obstacle = obstaclePool2.Dequeue();
        }

        if (obstacle != null)
        {
            obstacle.SetActive(true); // Activate the object
        }
        else
        {
            // Optionally instantiate more if the pool is empty
            if (obstacleType == 1)
            {
                obstacle = Instantiate(obstaclePrefab1);
            }
            else if (obstacleType == 2)
            {
                obstacle = Instantiate(obstaclePrefab2);
            }
        }

        return obstacle;
    }

    // Get a coin from the pool
    public GameObject GetCoinFromPool()
    {
        if (coinPool.Count > 0)
        {
            GameObject coin = coinPool.Dequeue();
            coin.SetActive(true); // Activate the object
            return coin;
        }
        else
        {
            // Optionally, instantiate more if the pool is empty
            GameObject newCoin = Instantiate(coinPrefab);
            return newCoin;
        }
    }

    // Return obstacle to the pool
    public void ReturnObstacleToPool(GameObject obstacle, int obstacleType)
    {
        obstacle.SetActive(false); // Deactivate the object and return it to the pool

        if (obstacleType == 1)
        {
            obstaclePool1.Enqueue(obstacle);
        }
        else if (obstacleType == 2)
        {
            obstaclePool2.Enqueue(obstacle);
        }
    }

    // Return coin to the pool
    public void ReturnCoinToPool(GameObject coin)
    {
        coin.SetActive(false); // Deactivate the object and return it to the pool
        coinPool.Enqueue(coin);
    }
}
