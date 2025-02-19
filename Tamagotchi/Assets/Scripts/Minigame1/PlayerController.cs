using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // This can be your player health or any condition for game over
    private bool isDead = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with an obstacle
        if (collision.gameObject.CompareTag("Obstacle")) // Ensure your obstacles are tagged as "Obstacle"
        {
            // Trigger Game Over (or whatever logic you want)
            GameOver();
        }
    }

    void GameOver()
    {
        if (!isDead)
        {
            isDead = true;
            // Show game over UI, pause the game, etc.
            // Example: Display Game Over Canvas
            Time.timeScale = 0;  // Pause the game
            GameObject gameOverCanvas = GameObject.Find("GameOverCanvas");
            if (gameOverCanvas != null)
            {
                gameOverCanvas.SetActive(true);
            }
            // Optionally, you can reload the scene after some time or show a restart button
        }
    }
}
