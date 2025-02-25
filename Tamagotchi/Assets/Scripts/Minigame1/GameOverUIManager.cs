using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Import TextMeshPro namespace

public class GameOverUIManager : MonoBehaviour
{
    public static GameOverUIManager Instance;  // Singleton instance

    public GameObject gameOverCanvas; // Assign this in the Inspector
    public TextMeshProUGUI finalScoreText; // Assign this in the Inspector

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameOverCanvas.SetActive(false); // Hide Game Over canvas at the start
    }

    public void ShowGameOverCanvas(int finalScore)
    {
        gameOverCanvas.SetActive(true);
        finalScoreText.text = $"Coins Collected: {finalScore}"; // Update with the final coin count
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void ReturnToMainRoom()
    {
        Time.timeScale = 1f; // Ensure time is resumed
        SceneManager.LoadScene("MainRoom"); // Load the Main Room scene
    }
}
