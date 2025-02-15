using UnityEngine;

public class DraggableShower : MonoBehaviour
{
    private Vector3 offset;

    public Sprite normalShowerheadSprite; // Reference to normal showerhead sprite
    public Sprite waterSprayingShowerheadSprite; // Reference to water spraying showerhead sprite
    private SpriteRenderer showerheadRenderer;

    public Tamagotchi tamagotchiScript; // Reference to the Tamagotchi script to interact with foam
    private bool isWaterSpraying = false; // Track if the showerhead is spraying water

    void Start()
    {
        // Get the SpriteRenderer component for sprite switching
        showerheadRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        // Check if the showerhead is over the foam (use colliders for collision detection)
        Collider2D foamCollider = tamagotchiScript.GetFoamCollider(); // Assuming Tamagotchi script has method to get foam collider

        if (foamCollider != null && foamCollider.IsTouching(GetComponent<Collider2D>()))
        {
            CleanFoam();
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoint.z = 0;
        return mousePoint;
    }

    // Switch the showerhead to water-spraying mode
    public void SwitchToWaterSpraying()
    {
        isWaterSpraying = true;
        showerheadRenderer.sprite = waterSprayingShowerheadSprite; // Change sprite to water spraying
    }

    // Switch back to normal showerhead mode
    public void SwitchToNormalShowerhead()
    {
        isWaterSpraying = false;
        showerheadRenderer.sprite = normalShowerheadSprite; // Change sprite to normal showerhead
    }

    // Clean foam if the showerhead sprays water over it
    private void CleanFoam()
    {
        if (isWaterSpraying)
        {
            // Notify the Tamagotchi script to clean foam
            tamagotchiScript.CleanFoamWithShower(gameObject); // This is where Tamagotchi cleans foam when water is sprayed
        }
    }
}
