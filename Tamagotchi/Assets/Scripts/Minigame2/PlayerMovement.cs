using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f;        // Speed of movement
    private float minX = -8f;           // Minimum X boundary
    private float maxX = 8f;            // Maximum X boundary
    public GameObject beamPrefab;       // The beam GameObject
    public float beamRange = 5f;        // How far the beam can reach downwards
    public LayerMask pickUpLayer;       // Layer to identify pickable objects

    private bool canMove = true;        // Flag to check if player can move
    private float beamCooldown = 1f;    // Time in seconds to disable movement after shooting the beam
    private float cooldownTimer = 0f;   // Timer to track cooldown

    private void Update()
    {
        // If cooldown timer is active, decrease it
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canMove = true; // Re-enable movement after cooldown
            }
        }

        // Handle player movement if allowed
        if (canMove)
        {
            MovePlayer();
        }

        // Shoot the beam when space or enter is pressed, and check if movement is allowed
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && canMove)
        {
            ShootBeam();
        }
    }

    private void MovePlayer()
    {
        // Get horizontal input (e.g., arrow keys, A/D keys)
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Calculate new position based on input
        Vector3 newPosition = transform.position + new Vector3(moveInput * moveSpeed * Time.deltaTime, 0f, 0f);

        // Clamp the X position between the minX and maxX values
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Apply the new position to the player object
        transform.position = newPosition;
    }

    private void ShootBeam()
    {
        // Disable movement temporarily
        canMove = false;
        cooldownTimer = beamCooldown;  // Start the cooldown timer

        // Adjust the spawn position with an offset
        Vector3 beamPosition = transform.position + new Vector3(0f, -4.2f, 0f); // Adjust the Y offset here

        // Instantiate the beam at the adjusted position
        GameObject beam = Instantiate(beamPrefab, beamPosition, Quaternion.identity);

        // Raycast downwards from the player's position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, beamRange, pickUpLayer);

        // If the beam hits an object, pick it up
        if (hit.collider != null)
        {
            Debug.Log("Picked up: " + hit.collider.name);
            // Your pickup logic here
            Destroy(hit.collider.gameObject); // Example: destroy the object
        }

        // Destroy the beam after 1 second
        Destroy(beam, 1f);
    }
}
