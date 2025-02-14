using UnityEngine;

public class Food : MonoBehaviour
{
    public enum FoodType { Water, HotDog, Cupcake }
    public FoodType foodType; // To differentiate between food types

    private bool isDropped = false; // To check if the food was dropped

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the food collided with the Tamagotchi
        if (other.CompareTag("Tamagotchi"))
        {
            // Feed the Tamagotchi
            FeedTamagotchi(other.gameObject);
        }
    }

    private void FeedTamagotchi(GameObject tamagotchi)
    {
        // Check if the player has enough coins for Hot Dog or Cupcake
        if (foodType == FoodType.HotDog || foodType == FoodType.Cupcake)
        {
            if (CoinManager.Instance.playerCoins >= 5)
            {
                CoinManager.Instance.SubtractCoins(5); // Deduct 5 coins for these foods
                Debug.Log($"{foodType} fed to Tamagotchi! Coins deducted.");
                Destroy(gameObject); // Remove food after feeding
            }
            else
            {
                Debug.Log("Not enough coins to feed Hot Dog or Cupcake!");
            }
        }
        else if (foodType == FoodType.Water)
        {
            // Water is free, no coin deduction
            Debug.Log("Water fed to Tamagotchi! No coins deducted.");
            Destroy(gameObject); // Remove food after feeding
        }
    }

    // This function can be used when the food is dropped on the floor
    public void DropFoodOnFloor()
    {
        // Make the food stay on the floor for a while (add a timer or make it visible)
        Debug.Log($"{foodType} dropped on the floor.");
    }
}
