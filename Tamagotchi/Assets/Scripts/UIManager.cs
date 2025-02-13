using UnityEngine;
using UnityEngine.UI; // For UI elements
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text coinDisplayText; // TextMesh Pro UI element for displaying the coin count

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of UIManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // This method is called when the scene starts to display the current coin count.
    public void Start()
    {
        // Make sure the UI displays the starting coin count (35)
        if (CoinManager.Instance != null)
        {
            UpdateCoinDisplay(CoinManager.Instance.playerCoins); // Update the UI with the initial value of coins
        }
    }

    // Update the coin display text on the UI
    public void UpdateCoinDisplay(int coins)
    {
        coinDisplayText.text = coins.ToString();
    }
}