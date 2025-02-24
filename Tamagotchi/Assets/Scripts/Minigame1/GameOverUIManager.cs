using UnityEngine;
using TMPro;

public class GameOverUIManager : MonoBehaviour
{
    public static GameOverUIManager Instance;

    public CanvasGroup gameOverCanvasGroup; // Canvas Group
    public TextMeshProUGUI coinText;

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

        if (gameOverCanvasGroup != null)
        {
            gameOverCanvasGroup.alpha = 0; // Make it invisible at start
            gameOverCanvasGroup.interactable = false;
            gameOverCanvasGroup.blocksRaycasts = false;
        }
        else
        {
            Debug.LogError("CanvasGroup is missing on GameOverCanvas!");
        }
    }

    public void ShowGameOverCanvas(int coins)
    {
        if (gameOverCanvasGroup != null)
        {
            gameOverCanvasGroup.alpha = 1;  // Show UI
            gameOverCanvasGroup.interactable = true;
            gameOverCanvasGroup.blocksRaycasts = true;
            coinText.text = "Coins: " + coins;
        }
        else
        {
            Debug.LogError("GameOverCanvas reference is missing!");
        }
    }

    public void HideGameOverCanvas()
    {
        if (gameOverCanvasGroup != null)
        {
            gameOverCanvasGroup.alpha = 0;  // Hide UI
            gameOverCanvasGroup.interactable = false;
            gameOverCanvasGroup.blocksRaycasts = false;
        }
    }
}
