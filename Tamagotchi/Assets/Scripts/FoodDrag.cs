using UnityEngine;

public class FoodDrag : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 initialPosition;

    private void OnMouseDown()
    {
        isDragging = true;
        initialPosition = transform.position; // Store initial position of food
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f); // Follow the mouse
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        // Check if the food is dropped on the Tamagotchi or floor
        // If it's dropped on the floor, invoke DropFoodOnFloor (you can later improve this)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f); // Detect objects within a small radius
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Tamagotchi"))
            {
                // The food is dropped on the Tamagotchi
                FeedTamagotchi(collider.gameObject);
            }
            else if (collider.CompareTag("Floor"))
            {
                // The food is dropped on the floor
                DropFoodOnFloor();
            }
        }

        // Reset position if not dropped correctly
        transform.position = initialPosition;
    }

    private void FeedTamagotchi(GameObject tamagotchi)
    {
        // Feed the Tamagotchi
        // You can call the Tamagotchi’s feeding method here
        tamagotchi.GetComponent<Tamagotchi>().Feed();

    }

    private void DropFoodOnFloor()
    {
        // Food stays on the floor until picked up again
        Debug.Log("Food dropped on the floor!");
    }
}
