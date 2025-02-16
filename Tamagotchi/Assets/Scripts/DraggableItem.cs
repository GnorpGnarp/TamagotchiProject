using UnityEngine;
using UnityEngine.EventSystems;  // Required for UI events

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition; // To store the original position of the soap
    private RectTransform rectTransform; // To access the RectTransform component for UI elements
    private Canvas parentCanvas; // Reference to the parent canvas to calculate relative position
    private bool isDragging = false; // To track whether the item is being dragged

    public Tamagotchi tamagotchiScript; // Reference to the Tamagotchi script to interact with
    public GameObject foamPrefab; // Reference to the foam prefab
    private GameObject foamInstance; // Store foam instance

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // Get RectTransform for UI elements
        originalPosition = rectTransform.position; // Store the initial position of the soap
        parentCanvas = GetComponentInParent<Canvas>(); // Get the parent canvas
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Soap clicked");
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Debug.Log("Dragging soap");
            // Update the position of the object based on mouse position
            Vector2 localPointerPosition = eventData.position / parentCanvas.scaleFactor; // Adjust for canvas scale
            rectTransform.position = localPointerPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Soap released");
        isDragging = false;

        // Check if the soap is over the Tamagotchi (use colliders to detect interaction)
        if (IsOverTamagotchi())
        {
            ApplySoapToTamagotchi();
        }

        // Snap back to original position
        rectTransform.position = originalPosition;
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
            foamInstance = Instantiate(foamPrefab, rectTransform.position, Quaternion.identity);
            foamInstance.SetActive(true);  // Show foam when soap is applied
            tamagotchiScript.ApplySoap(rectTransform.position); // Notify Tamagotchi to apply soap and spawn foam
        }
    }
}
