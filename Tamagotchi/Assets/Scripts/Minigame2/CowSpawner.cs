using UnityEngine;

public class CowSpawner : MonoBehaviour
{
    public GameObject cowPrefab;  // Cow prefab
    public float spawnRate = 2f;  // Time between each spawn
    public float spawnAreaWidth = 8f;  // Width of the spawn area (e.g., between -5 and 5 on the X-axis)
    public float spawnAreaHeight = -2f; // Height of the spawn area

    private void Start()
    {
        // Start spawning cows at regular intervals
        InvokeRepeating("SpawnCow", 0f, spawnRate);
    }

    private void SpawnCow()
    {
        // Random position within the spawn area
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnAreaWidth, spawnAreaWidth), spawnAreaHeight, 0f);

        // Instantiate a new cow at the random position
        GameObject newCow = Instantiate(cowPrefab, spawnPosition, Quaternion.identity);

        // Optional: set the cow's layer to "Cows"
        newCow.layer = LayerMask.NameToLayer("Cows");
    }
}
