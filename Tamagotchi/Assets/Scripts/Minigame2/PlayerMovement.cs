using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float moveSpeed = 5f;        // Speed of movement
    private float minX = -8f;           // Minimum X boundary
    private float maxX = 8f;            // Maximum X boundary
    public GameObject beamPrefab;       // The beam GameObject
    private bool canMove = true;        // Controls if player can move
    private float beamCooldown = 1f;    // Time before movement resumes
    private float cooldownTimer = 0f;   // Timer for cooldown

    private void Update()
    {
        // Handle movement cooldown
        if (!canMove)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canMove = true;  // Re-enable movement after cooldown
            }
        }

        // Handle player movement
        if (canMove)
        {
            MovePlayer();
        }

        // Shoot the beam when space or enter is pressed
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && canMove)
        {
            ShootBeam();
        }
    }

    private void MovePlayer()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector3 newPosition = transform.position + new Vector3(moveInput * moveSpeed * Time.deltaTime, 0f, 0f);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;
    }

    private void ShootBeam()
    {
        canMove = false;              // Disable movement
        cooldownTimer = beamCooldown; // Start cooldown timer

        Vector3 beamPosition = transform.position + new Vector3(0f, -4.2f, 0f);
        GameObject beam = Instantiate(beamPrefab, beamPosition, Quaternion.identity);

        Destroy(beam, 1f); // Destroy the beam after 1 second
    }
}
