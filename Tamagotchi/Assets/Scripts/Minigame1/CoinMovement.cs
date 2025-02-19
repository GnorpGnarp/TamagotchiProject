using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        // Move the coin upwards
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // If the coin goes off-screen (y > 7f), return it to the pool
        if (transform.position.y > 7f)
        {
            ObjectPooler objectPooler = FindObjectOfType<ObjectPooler>();
            objectPooler.ReturnCoinToPool(gameObject);
        }
    }

    // Set the speed for the coin
    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
