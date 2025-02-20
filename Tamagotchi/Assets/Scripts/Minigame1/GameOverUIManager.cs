using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // To access the Button component

public class GameOverUIManager : MonoBehaviour
{
    public GameObject gameOverCanvas;  // Reference to the Game Over UI Canvas
    public TextMeshProUGUI coinText;  // Reference to the TextMeshProUGUI component that shows the coin count
    public Button resetButton;  // Reference to the Reset button
    public Button returnButton;  // Reference to the Return button

    // Singleton pattern to ensure only one instance of GameOverUIManager exists
    public static GameOverUIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("GameOverUIManager instance initialized.");
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }

        // Add listeners for buttons
        resetButton.onClick.AddListener(ResetLevel);
        returnButton.onClick.AddListener(ReturnToMainRoom);
    }
    public void GameOver()
    {
        GameOverUIManager gameOverUI = FindObjectOfType<GameOverUIManager>();
        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOverCanvas(CoinManager.Instance.playerCoins);
            Time.timeScale = 0f;  // Freeze the game
        }
        else
        {
            Debug.LogError("GameOverUIManager not found! Ensure it exists in the scene.");
        }
    }


    // Show the Game Over canvas and display the coins collected
    public void ShowGameOverCanvas(int playerCoins)
    {
        gameOverCanvas.SetActive(true);
        coinText.text = "Coins Collected: " + playerCoins;
    }

    // Hide the Game Over canvas (for example, after restarting or returning to the main menu)
    public void HideGameOverCanvas()
    {
        gameOverCanvas.SetActive(false);
    }

    // Button action: Reset the level and play again
    private void ResetLevel()
    {
        // Optionally, reset variables like score, player health, etc.
        // Reload the current scene to restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;  // Unfreeze the game if it was paused
    }

    // Button action: Return to the Main Room scene
    private void ReturnToMainRoom()
    {
        // Load the main menu or the main room scene
        SceneManager.LoadScene("MainRoom");  // Replace with the actual scene name if necessary
        Time.timeScale = 1f;  // Unfreeze the game if it was paused
    }
}
