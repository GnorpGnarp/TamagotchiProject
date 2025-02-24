using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement

    private Vector2 moveDirection; // Movement direction

    void Update()
    {
        // Get input from arrow keys or WASD keys
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Move the player
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Clamp the player's position to limit the movement
        float clampedX = Mathf.Clamp(transform.position.x, -5f, 5f);
        float clampedY = Mathf.Clamp(transform.position.y, -4f, 2f);

        // Apply the clamped position to the player
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Trigger Game Over
            GameOver();
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            // Add coins to the player's balance
            CoinManager.Instance.AddCoins(1);  // Add 1 coin for each collected coin
            Destroy(collision.gameObject);  // Destroy the coin
        }
    }


    private void GameOver()
    {
        // Try to find the GameOverUIManager
        GameOverUIManager gameOverUI = FindObjectOfType<GameOverUIManager>();

        if (gameOverUI != null)
        {
            // Show Game Over canvas and update coin count
            gameOverUI.ShowGameOverCanvas(CoinManager.Instance.playerCoins);
            Time.timeScale = 0f;  // Freeze the game
        }
        else
        {
            // Log an error if GameOverUIManager is not found
            Debug.LogError("GameOverUIManager not found! Ensure it exists in the scene.");
        }
    }

}
