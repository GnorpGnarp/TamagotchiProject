using System.Collections.Generic;
using UnityEngine;

public class FoodPooler : MonoBehaviour
{
    public static FoodPooler Instance;

    public GameObject waterPrefab;  // Prefab for Water
    public GameObject hotDogPrefab; // Prefab for HotDog
    public GameObject cupcakePrefab; // Prefab for Cupcake

    private Queue<GameObject> waterPool = new Queue<GameObject>();
    private Queue<GameObject> hotDogPool = new Queue<GameObject>();
    private Queue<GameObject> cupcakePool = new Queue<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Method to get food from the pool based on the food type
    public GameObject GetPooledFood(Food.FoodType foodType)
    {
        GameObject food = null;

        switch (foodType)
        {
            case Food.FoodType.Water:
                if (waterPool.Count > 0)
                {
                    food = waterPool.Dequeue();
                    food.SetActive(true);
                }
                else
                {
                    food = Instantiate(waterPrefab);
                }
                break;

            case Food.FoodType.HotDog:
                if (hotDogPool.Count > 0)
                {
                    food = hotDogPool.Dequeue();
                    food.SetActive(true);
                }
                else
                {
                    food = Instantiate(hotDogPrefab);
                }
                break;

            case Food.FoodType.Cupcake:
                if (cupcakePool.Count > 0)
                {
                    food = cupcakePool.Dequeue();
                    food.SetActive(true);
                }
                else
                {
                    food = Instantiate(cupcakePrefab);
                }
                break;
        }

        return food;
    }

    // Method to return food to the appropriate pool
    public void ReturnFoodToPool(GameObject food, Food.FoodType foodType)
    {
        food.SetActive(false);

        switch (foodType)
        {
            case Food.FoodType.Water:
                waterPool.Enqueue(food);
                break;

            case Food.FoodType.HotDog:
                hotDogPool.Enqueue(food);
                break;

            case Food.FoodType.Cupcake:
                cupcakePool.Enqueue(food);
                break;
        }
    }
}
