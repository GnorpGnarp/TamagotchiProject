using UnityEngine;
using TMPro;  // For TextMeshPro

public class TimeLimit : MonoBehaviour
{
    public float timeLimit = 60f;  // 1 minute in seconds
    private float timeRemaining;
    public TextMeshProUGUI timerText;  // Reference to a TextMeshProUGUI component

    void Start()
    {
        timeRemaining = timeLimit;
    }

    void Update()
    {
        // Decrease the timer each frame
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            TimeUp();
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(timeRemaining);  // Get the seconds as an integer
            timerText.text = seconds.ToString();  // Display only seconds
        }
    }

    void TimeUp()
    {
        Debug.Log("Time's up!");

        if (GameOverUIManager.Instance != null)
        {
            GameOverUIManager.Instance.ShowGameOverCanvas(CoinManager.Instance?.playerCoins ?? 0);
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Debug.LogError("GameOverUIManager.Instance is null! Make sure it's in the scene.");
        }
    }
}
