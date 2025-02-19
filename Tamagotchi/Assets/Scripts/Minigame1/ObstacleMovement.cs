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
            // Return obstacle to the pool (provide the obstacle type)
            ObjectPooler objectPooler = FindObjectOfType<ObjectPooler>();
            objectPooler.ReturnObstacleToPool(gameObject, 1); // Pass the obstacle type (1 or 2)
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}

