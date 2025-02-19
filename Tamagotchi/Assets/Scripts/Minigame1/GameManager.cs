using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Spawner obstacleSpawner;
    public CoinSpawner coinSpawner;

    public void EndGame()
    {
        // Stop both spawners when the game ends
        obstacleSpawner.StopSpawning();
        coinSpawner.StopSpawning();
        // Additional game over logic (e.g., show game over screen, etc.)
        Debug.Log("Game Over!");
        //add turning on game over canvas
    }
}
