using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public ObjectPooler objectPooler;
    public float spawnRate = 2f; 
    public Transform ySpawnLimit; 
    private bool gameIsOver = false; 
    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    private IEnumerator SpawnCoins()
    {
        while (!gameIsOver)
        {
         
            GameObject coin = objectPooler.GetCoinFromPool();

            
            Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), ySpawnLimit.position.y, 0f);
            coin.transform.position = spawnPosition;

           
            CoinMovement coinMovement = coin.GetComponent<CoinMovement>();
            if (coinMovement != null)
            {
                coinMovement.SetSpeed(1f); 
            }

            yield return new WaitForSeconds(spawnRate); 
        }
    }

    
    public void StopSpawning()
    {
        gameIsOver = true;
    }
}
