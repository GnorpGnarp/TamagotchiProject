using UnityEngine;
using static Food;

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
            // Check if the food is dropped on the Tamagotchi
            Collider2D tamagotchiCollider = GameObject.FindGameObjectWithTag("Tamagotchi").GetComponent<Collider2D>();

            // Check if the food has collided with the Tamagotchi collider
            if (tamagotchiCollider != null && tamagotchiCollider.bounds.Contains(transform.position))
            {
                // If it's on the Tamagotchi, feed it
                Food foodScript = GetComponent<Food>();
                foodScript.FeedTamagotchi();
            }
            else
            {
                // If not on Tamagotchi, drop it on the floor
                DropFoodOnFloor();
            }
        }

        isDragging = false;
    }

    private void DropFoodOnFloor()
    {
        // Logic to drop the food to the floor
        transform.position = new Vector3(transform.position.x, -4f, transform.position.z); // or your preferred Y position
                                                                                           // Do not return to pool here yet, only return when fed!
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
