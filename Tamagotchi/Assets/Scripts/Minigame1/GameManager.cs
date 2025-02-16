using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Spawner spawner;

    public void EndGame()
    {
        // Stop spawning obstacles and coins
        spawner.StopSpawning();

        // Additional game over logic (e.g., show game over screen, etc.)
        Debug.Log("Game Over!");
    }
}
