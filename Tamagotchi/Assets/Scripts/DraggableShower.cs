using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableShower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 offset;

    public Sprite normalShowerheadSprite; // Reference to normal showerhead sprite
    public Sprite waterSprayingShowerheadSprite; // Reference to water spraying showerhead sprite
    private SpriteRenderer showerheadRenderer;

    public Tamagotchi tamagotchiScript; // Reference to the Tamagotchi script to interact with foam
    private bool isWaterSpraying = false; // Track if the showerhead is spraying water

    private Vector3 originalPosition; // Store original position to reset

    void Start()
    {
        showerheadRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Change to water-spraying sprite when picked up
        SwitchToWaterSpraying();
        offset = transform.position - GetMouseWorldPos();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset position and sprite
        transform.position = originalPosition;
        SwitchToNormalShowerhead();

        // Check if showerhead is over foam (clean foam)
        Collider2D foamCollider = tamagotchiScript.GetFoamCollider();
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

    public void SwitchToWaterSpraying()
    {
        isWaterSpraying = true;
        showerheadRenderer.sprite = waterSprayingShowerheadSprite;
    }

    public void SwitchToNormalShowerhead()
    {
        isWaterSpraying = false;
        showerheadRenderer.sprite = normalShowerheadSprite;
    }

    private void CleanFoam()
    {
        if (isWaterSpraying)
        {
            tamagotchiScript.CleanFoamWithShower(gameObject);
        }
    }
}
