using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1; // The value of each coin picked up

    // When the player collides with the coin
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Access the CoinManager and add the coin
            CoinManager coinManager = FindObjectOfType<CoinManager>();
            if (coinManager != null)
            {
                coinManager.AddCoins(coinValue);
            }

            // Destroy the coin after picking it up
            Destroy(gameObject);
        }
    }
}
