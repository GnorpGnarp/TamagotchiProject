using UnityEngine;
using UnityEngine.UI;

public class FoodSpawner : MonoBehaviour
{
    public GameObject waterPrefab; // Reference to the water prefab
    public GameObject hotDogPrefab; // Reference to the hot dog prefab
    public GameObject cupcakePrefab; // Reference to the cupcake prefab
    public Transform spawnPoint; // Point where the food will appear

    public Button waterButton; // Reference to the Water button
    public Button hotDogButton; // Reference to the Hot Dog button
    public Button cupcakeButton; // Reference to the Cupcake button

    void Start()
    {
        // Add listeners for button clicks to spawn food
        waterButton.onClick.AddListener(SpawnWater);
        hotDogButton.onClick.AddListener(SpawnHotDog);
        cupcakeButton.onClick.AddListener(SpawnCupcake);
    }

    void SpawnWater()
    {
        Instantiate(waterPrefab, spawnPoint.position, Quaternion.identity); // Spawn water
        Debug.Log("Water spawned!");
    }

    void SpawnHotDog()
    {
        Instantiate(hotDogPrefab, spawnPoint.position, Quaternion.identity); // Spawn Hot Dog
        Debug.Log("Hot Dog spawned!");
    }

    void SpawnCupcake()
    {
        Instantiate(cupcakePrefab, spawnPoint.position, Quaternion.identity); // Spawn Cupcake
        Debug.Log("Cupcake spawned!");
    }
}
