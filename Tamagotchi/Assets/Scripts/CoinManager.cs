using UnityEngine;

public class CoinManager : MonoBehaviour
{
   
    public static CoinManager Instance;  // Singleton pattern to ensure only one CoinManager instance exists

    public int playerCoins = 35;  // Starting coin amount

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of CoinManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // This method will run when the scene starts, after all objects are initialized
    private void Start()
    {
        // Initialize the coin display at the start of the game
        if (UIManager.Instance != null) // Check if UIManager exists
        {
            UIManager.Instance.UpdateCoinDisplay(playerCoins);  // Update the UI with the initial value of coins
        }
    }

    // This method adds coins to the player's balance
    public void AddCoins(int amount)
    {
        playerCoins += amount;
        UIManager.Instance.UpdateCoinDisplay(playerCoins);  // Update the UI whenever coins are added
    }

    // This method subtracts coins (if needed)
    public void SubtractCoins(int amount)
    {
        if (playerCoins >= amount)
        {
            playerCoins -= amount;
            UIManager.Instance.UpdateCoinDisplay(playerCoins);  // Update the UI whenever coins are subtracted
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}