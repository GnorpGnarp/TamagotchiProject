using UnityEngine;

public class FoodDrag : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        if (mainCamera != null)
        {
            offset = transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        isDragging = true;
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            // Call the FeedTamagotchi function when the food is released
            GameObject tamagotchiObject = GameObject.Find("Tamagotchi");
            Food foodScript = GetComponent<Food>();
            foodScript.FeedTamagotchi();
        }
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }
}
