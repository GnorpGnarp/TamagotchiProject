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
        // Logic when time is up (e.g., end the game or show results)
        Debug.Log("Time's up!");
        // Add your game over or time-up handling code here
    }
}
