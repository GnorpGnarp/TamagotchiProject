using UnityEngine;

public class TamagotchiMovement : MonoBehaviour
{
    public Transform character;        // Reference to the child (your character)
    public float startX = 367f;        // Starting position on the x-axis
    public float minX = 350f;          // Minimum boundary (left edge)
    public float maxX = 400f;          // Maximum boundary (right edge)
    public float moveSpeed = 2f;       // Speed of movement
    public float jumpHeight = 0.5f;    // Maximum jump height
    public float jumpFrequency = 0.5f; // Frequency of jumps (lower = less frequent)
    public float directionChangeTime = 2f; // Time before changing direction

    private float currentX;            // Current position on the x-axis
    private bool isJumping = false;
    private float directionTimer = 0f; // Timer to control when direction changes
    private float moveDirection = 1f; // 1 for right, -1 for left

    void Start()
    {
        // Set the starting position and ensure it's within the boundaries
        currentX = Mathf.Clamp(startX, minX, maxX);
        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        // Randomly change movement direction after a set time
        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0)
        {
            // Change direction randomly
            moveDirection = Random.Range(0f, 1f) > 0.5f ? 1f : -1f;
            directionTimer = directionChangeTime; // Reset timer
        }

        // Move the parent object (empty GameObject) along the x-axis
        currentX += moveSpeed * moveDirection * Time.deltaTime;

        // Check if the character hits the left or right boundary (minX or maxX)
        if (currentX <= minX || currentX >= maxX)
        {
            // Reverse direction to simulate bouncing
            moveDirection = -moveDirection;
        }

        // Clamp the currentX position to stay within the boundaries
        currentX = Mathf.Clamp(currentX, minX, maxX);

        // Apply random "jump" effect (a bit smoother with sinusoidal motion)
        if (Random.value < jumpFrequency)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        // Apply movement and jump to the child (the character)
        float jumpOffset = isJumping ? Mathf.Sin(Time.time * 3) * jumpHeight : 0f;

        // Set the new position of the character (child)
        character.localPosition = new Vector3(0, jumpOffset, 0);

        // Move the parent (empty object) on the x-axis
        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
    }
}
