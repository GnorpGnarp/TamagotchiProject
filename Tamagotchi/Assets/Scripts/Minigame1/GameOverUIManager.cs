using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
public class GameOverUIManager : MonoBehaviour
{
    public static GameOverUIManager Instance;  

    public GameObject gameOverCanvas; 
    public TextMeshProUGUI finalScoreText; 

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
        gameOverCanvas.SetActive(false); 
    }

    public void ShowGameOverCanvas(int finalScore)
    {
        gameOverCanvas.SetActive(true);
        finalScoreText.text = $"Coins Collected: {finalScore}"; 
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
