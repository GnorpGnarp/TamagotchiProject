using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameManager gameManager; // Reference to the game manager to stop the spawner

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop spawning obstacles and end the game
            gameManager.EndGame();
        }
    }
}
