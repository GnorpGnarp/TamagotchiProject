using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D normalCursor;   
    public Texture2D clickedCursor;  
    public Vector2 hotSpot = Vector2.zero; 

    void Start()
    {
        Cursor.SetCursor(normalCursor, hotSpot, CursorMode.Auto);
    }

    void Update()
    {
       
        if (Input.GetMouseButton(0))  
        {
            Cursor.SetCursor(clickedCursor, hotSpot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(normalCursor, hotSpot, CursorMode.Auto);
        }
    }
}
