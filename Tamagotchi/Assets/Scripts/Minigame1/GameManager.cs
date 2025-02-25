using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Spawner obstacleSpawner;
    public CoinSpawner coinSpawner;

    public void EndGame()
    {
        
        obstacleSpawner.StopSpawning();
        coinSpawner.StopSpawning();
       
        Debug.Log("Game Over!");
       
    }
}
