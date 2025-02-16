using UnityEngine;

public class DraggableShower : MonoBehaviour
{
    private Vector3 originalPosition; // To store the original position of the showerhead
    private Vector3 offset;
    private bool isDragging = false;

    public Sprite normalShowerheadSprite; // Normal showerhead sprite
    public Sprite waterSprayingShowerheadSprite; // Water spraying showerhead sprite
    private SpriteRenderer showerheadRenderer;

    public Tamagotchi tamagotchiScript; // Reference to Tamagotchi to interact with foam
    private bool isWaterSpraying = false;

    void Start()
    {
        originalPosition = transform.position; // Store the initial position of the showerhead
        showerheadRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        SwitchToNormalShowerhead(); // Ensure it's using the normal sprite by default
    }

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        SwitchToWaterSpraying(); // Switch to water-spraying when the showerhead is picked up
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            newPosition.z = 0;  // Keep it in 2D space
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Snap back to the original position when released
        transform.position = originalPosition;

        // Optionally, check if the showerhead is over foam and clean it
        Collider2D foamCollider = tamagotchiScript.GetFoamCollider();
        if (foamCollider != null && foamCollider.IsTouching(GetComponent<Collider2D>()))
        {
            CleanFoam();
        }

        SwitchToNormalShowerhead(); // Reset the sprite to normal after releasing
    }

    // Switch to water-spraying mode (when the showerhead is picked up)
    public void SwitchToWaterSpraying()
    {
        isWaterSpraying = true;
        showerheadRenderer.sprite = waterSprayingShowerheadSprite; // Switch sprite to water-spraying
    }

    // Switch back to normal showerhead mode (when released)
    public void SwitchToNormalShowerhead()
    {
        isWaterSpraying = false;
        showerheadRenderer.sprite = normalShowerheadSprite; // Switch back to normal showerhead
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Foam")) // Make sure your foam has the "Foam" tag
        {
            CleanFoam();
        }
    }

    void CleanFoam()
    {
        if (isWaterSpraying)
        {
            tamagotchiScript.CleanFoamWithShower(gameObject); // Notify Tamagotchi to clean foam
        }
    }

}
