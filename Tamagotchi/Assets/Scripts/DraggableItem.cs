using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 offset;
    private bool isDragging = false;

    public Tamagotchi tamagotchiScript; // Reference to Tamagotchi to interact with
    public GameObject foamPrefab; // Reference to foam prefab
    private GameObject foamInstance; // Foam instance holder

    void Start()
    {
        originalPosition = transform.position; // Save the original position of the soap
    }

    void OnMouseDown()
    {
        Debug.Log("Soap clicked");
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            newPosition.z = 0;  // Ensure the soap stays in 2D space
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        Debug.Log("Soap released");
        isDragging = false;

        // Check if the soap is over the Tamagotchi
        if (IsOverTamagotchi())
        {
            ApplySoapToTamagotchi();
        }

        // Optionally, snap back to original position
        transform.position = originalPosition;
    }

    private bool IsOverTamagotchi()
    {
        Collider2D tamagotchiCollider = tamagotchiScript.GetComponent<Collider2D>();
        Collider2D soapCollider = GetComponent<Collider2D>();

        return soapCollider.IsTouching(tamagotchiCollider);
    }

    private void ApplySoapToTamagotchi()
    {
        if (foamInstance == null)
        {
            foamInstance = Instantiate(foamPrefab, transform.position, Quaternion.identity);
            foamInstance.SetActive(true);  // Make foam visible
            tamagotchiScript.ApplySoap(transform.position); // Notify Tamagotchi
        }
    }
}
