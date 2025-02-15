using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Vector3 originalPosition; // To store the original position of the soap
    private Vector3 offset; // The offset between the mouse position and the object
    private bool isDragging = false; // To track whether the item is being dragged

    public Tamagotchi tamagotchiScript; // Reference to the Tamagotchi script to interact with
    public GameObject foamPrefab; // Reference to the foam prefab
    private GameObject foamInstance; // Store foam instance

    private void Start()
    {
        originalPosition = transform.position; // Store the initial position of the soap
    }

    private void OnMouseDown()
    {
        Debug.Log("Soap clicked");
        // When the user clicks on the soap, start dragging
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Debug.Log("Dragging soap");
            // Update the position of the object based on the mouse position
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            newPosition.z = 0; // Ensure it's in 2D space
            transform.position = newPosition;
        }
    }

    private void OnMouseUp()
    {
        Debug.Log("Soap released");
        // When the user releases the mouse, stop dragging and return the object to its original position
        isDragging = false;

        // Check if the soap is over the Tamagotchi (use colliders to detect interaction)
        if (IsOverTamagotchi())
        {
            ApplySoapToTamagotchi();
        }

        // Snap back to original position
        transform.position = originalPosition;
    }

    private bool IsOverTamagotchi()
    {
        // Check if the soap collider is touching the Tamagotchi collider
        Collider2D tamagotchiCollider = tamagotchiScript.GetComponent<Collider2D>(); // Assuming Tamagotchi has a collider
        Collider2D soapCollider = GetComponent<Collider2D>();

        return soapCollider.IsTouching(tamagotchiCollider);
    }

    private void ApplySoapToTamagotchi()
    {
        // Apply soap by spawning foam
        if (foamInstance == null)
        {
            foamInstance = Instantiate(foamPrefab, transform.position, Quaternion.identity);
            foamInstance.SetActive(true);  // Show foam when soap is applied
            tamagotchiScript.ApplySoap(transform.position); // Notify Tamagotchi to apply soap and spawn foam
        }
    }
}
