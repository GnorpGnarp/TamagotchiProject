using UnityEngine;

public class Food : MonoBehaviour
{
    // Enum to differentiate food types
    public enum FoodType { Water, HotDog, Cupcake }
    public FoodType foodType;

    // Reference to Tamagotchi script or GameObject
    private Tamagotchi tamagotchi;
    [SerializeField] private GameObject tamagotchiObject;  // This will be set dynamically at runtime

    private bool isDropped = false; // To check if the food was dropped

    void Start()
    {
        // Dynamically find the Tamagotchi object in the scene
        if (tamagotchiObject == null)
        {
            tamagotchiObject = GameObject.Find("Tamagotchi"); // Ensure this matches the name of your Tamagotchi GameObject
        }

        // Deduct coins when the food is spawned
        DeductCoinsOnSpawn();
    }

    // This is where you can implement your coin deduction logic
    private void DeductCoinsOnSpawn()
    {
        // Your logic for deducting coins when food is spawned
        Debug.Log("Coins deducted!");
    }

    // Example of a method for feeding the Tamagotchi when colliding with it
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only feed the Tamagotchi if food hasn't been dropped yet
        if (!isDropped && other.CompareTag("Tamagotchi"))
        {
            FeedTamagotchi(); // Call feed method when colliding with Tamagotchi
            Debug.Log("Food collided with Tamagotchi! Feeding...");
        }
        else
        {
            // Drop the food on the floor
            DropFood();
            Debug.Log("Food dropped on the floor.");
        }
    }

    // Method to reset hunger when food collides with Tamagotchi
    public void FeedTamagotchi()
    {
        // Check if tamagotchiObject is assigned
        if (tamagotchiObject == null)
        {
            Debug.LogError("Tamagotchi object is null!");
            return; // Exit early to prevent further issues
        }

        // Try to get the Tamagotchi component
        tamagotchi = tamagotchiObject.GetComponent<Tamagotchi>();

        // Check if tamagotchi is assigned properly
        if (tamagotchi == null)
        {
            Debug.LogError("Tamagotchi component not found on tamagotchiObject.");
            return; // Exit early if Tamagotchi is not found
        }

        // Now call the Feed method
        try
        {
            tamagotchi.Feed();
            // After feeding, return food to pool
            FoodPooler.Instance.ReturnFoodToPool(this.gameObject, foodType); // Return to pool after feeding
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error feeding Tamagotchi: " + ex.Message);
        }
    }


    // Drop the food to the floor (set its position to y = -4)
    private void DropFood()
    {
        transform.position = new Vector3(transform.position.x, -4f, transform.position.z); // Drop to y = -4
        isDropped = true; // Mark it as dropped
    }

    // Return food to the pool and deactivate it
    private void ReturnToFoodPool()
    {
        // Assuming you have a pooler manager that handles returning objects to the pool
        FoodPooler.Instance.ReturnFoodToPool(this.gameObject, foodType);  // Return food to the pooler with the correct food type
        gameObject.SetActive(false); // Deactivate the food object in the scene
    }
}
