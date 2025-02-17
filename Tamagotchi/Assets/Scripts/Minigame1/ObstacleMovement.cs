using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float moveSpeed;

    void Update()
    {
        // Move the obstacle upwards
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Check if the obstacle has gone off-screen (Y > 7)
        if (transform.position.y > 7f)
        {
            // Destroy the obstacle if it goes out of frame
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
