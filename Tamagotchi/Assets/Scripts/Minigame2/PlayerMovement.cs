using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f;        // Speed of movement
    private float minX = -8f;           // Minimum X boundary
    private float maxX = 8f;            // Maximum X boundary
    public GameObject beamPrefab;       // The beam GameObject
    public float beamRange = 5f;        // How far the beam can reach downwards
    public LayerMask pickUpLayer;       // Layer to identify pickable objects

    private void Update()
    {
        // Handle player movement
        MovePlayer();

        // Shoot the beam when space or enter is pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
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
        // Adjust the spawn position with an offset
        Vector3 beamPosition = transform.position + new Vector3(0f, -0.5f, 0f); // Adjust the Y offset here

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