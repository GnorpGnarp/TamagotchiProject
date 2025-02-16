using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Vector3 originalPosition; // To store the original position of the soap
    private Vector3 offset; // The offset between the mouse position and the object
    private bool isDragging = false; // To track whether the item is being dragged

    public Tamagotchi tamagotchiScript; // Reference to the Tamagotchi script
    public GameObject foamPrefab; // Reference to the foam prefab
    private GameObject foamInstance; // Store foam instance

    void Start()
    {
        originalPosition = transform.position; // Store the initial position of the soap
    }

    void OnMouseDown()
    {
        // Start dragging
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // Update the position of the object based on the mouse position
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            newPosition.z = 0; // Ensure it's in 2D space
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        // Stop dragging and check if soap is over Tamagotchi
        isDragging = false;
        transform.position = originalPosition; // Snap back to the original position
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the soap collides with the Tamagotchi's collider
        if (other.CompareTag("Tamagotchi")) // Make sure your Tamagotchi has the "Tamagotchi" tag
        {
            ApplySoapToTamagotchi();
        }
    }

    void ApplySoapToTamagotchi()
    {
        // Spawn foam when soap is applied to the Tamagotchi
        if (foamInstance == null)
        {
            Vector3 spawnPosition = tamagotchiScript.transform.position; // Get the Tamagotchi's position
            foamInstance = Instantiate(foamPrefab, spawnPosition, Quaternion.identity);
            foamInstance.SetActive(true); // Activate foam instance
            tamagotchiScript.ApplySoap(spawnPosition); // Notify Tamagotchi to apply soap
        }
    }

}
