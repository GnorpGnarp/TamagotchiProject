using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    public void SpawnerFood(Food.FoodType foodType)
    {
        int foodCost = GetFoodCost(foodType);

        if (foodCost > 0 && CoinManager.Instance.playerCoins < foodCost)
        {
            Debug.Log("Not enough coins to buy this food!");
            return; // Prevent spawning if not enough coins
        }

        // Deduct coins first
        CoinManager.Instance.SubtractCoins(foodCost);

        // Get the food object from the pool and set its position
        GameObject newFood = FoodPooler.Instance.GetPooledFood(foodType);
        if (newFood != null)
        {
            newFood.transform.position = spawnPoint.position; // Set spawn position
            newFood.SetActive(true);
        }
    }

    private int GetFoodCost(Food.FoodType foodType)
    {
        switch (foodType)
        {
            case Food.FoodType.Water:
                return 0;
            case Food.FoodType.Cupcake:
            case Food.FoodType.HotDog:
                return 5;
            default:
                return 0;
        }
    }
}
