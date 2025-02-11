using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D normalCursor;    // Pointing finger cursor
    public Texture2D clickedCursor;   // Finger down cursor
    public Vector2 hotSpot = Vector2.zero;  // Center of the cursor texture

    void Start()
    {
        // Set the default cursor when the game starts
        Cursor.SetCursor(normalCursor, hotSpot, CursorMode.Auto);
    }

    void Update()
    {
        // Check for mouse click (you can use other input conditions if needed)
        if (Input.GetMouseButton(0))  // Left mouse button (0) is held down
        {
            Cursor.SetCursor(clickedCursor, hotSpot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(normalCursor, hotSpot, CursorMode.Auto);
        }
    }
}
