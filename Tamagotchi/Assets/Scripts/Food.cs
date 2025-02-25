using UnityEngine;

public class Food : MonoBehaviour
{
    public enum FoodType { Water, HotDog, Cupcake }
    public FoodType foodType; // To differentiate between food types

    private bool isDropped = false; // To check if the food was dropped

    private void Start()
    {
        // Deduct coins when the food is spawned
        DeductCoinsOnSpawn();
    }

    private void DeductCoinsOnSpawn()
    {
        // Check if the food is one that requires coins and deduct them immediately
        if (foodType == FoodType.HotDog || foodType == FoodType.Cupcake)
        {
            if (CoinManager.Instance.playerCoins >= 5)
            {
                CoinManager.Instance.SubtractCoins(5); // Deduct 5 coins for these foods
                Debug.Log($"{foodType} spawned! Coins deducted.");
            }
            else
            {
                Debug.Log("Not enough coins to spawn Hot Dog or Cupcake!");
                Destroy(gameObject); // Destroy the food if not enough coins
            }
        }
        // No coin deduction needed for Water
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Food collided with: {other.gameObject.name}"); // Log collision detection

        if (other.CompareTag("Tamagotchi"))
        {
            Debug.Log("Tamagotchi detected! Feeding...");
            FeedTamagotchi(other.gameObject);
        }
    }



    private void FeedTamagotchi(GameObject tamagotchi)
    {
        // Check if the Tamagotchi script is attached and call Feed()
        Tamagotchi tamagotchiScript = tamagotchi.GetComponent<Tamagotchi>();
        if (tamagotchiScript != null)
        {
            tamagotchiScript.Feed();
            Debug.Log($"{foodType} fed to Tamagotchi! Hunger reset to 100%");
        }

        Destroy(gameObject); // Remove food after feeding
    }


    // This function can be used when the food is dropped on the floor
    public void DropFoodOnFloor()
    {
        // Make the food stay on the floor for a while (add a timer or make it visible)
        Debug.Log($"{foodType} dropped on the floor.");
    }
}
