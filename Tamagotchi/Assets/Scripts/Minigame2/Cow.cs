using UnityEngine;

public class Cow : MonoBehaviour
{
    public float shrinkSpeed = 0.1f;  // Speed at which the cow shrinks
    public float moveSpeed = 1f;      // Speed at which the cow moves upwards
    private bool isBeingSucked = false; // Check if the cow is being sucked into the beam

    private void Update()
    {
        // If the cow is being sucked into the beam, shrink and move upwards
        if (isBeingSucked)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, shrinkSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), moveSpeed * Time.deltaTime);

            // If the cow is small enough, destroy it (it has been picked up)
            if (transform.localScale.x <= 0.05f) // If the cow is nearly invisible
            {
                Destroy(gameObject);
                CoinManager.Instance.AddCoins(1); // Add 1 coin for each cow picked up
            }
        }
    }

    // Call this method to start the sucking effect
    public void StartBeingSucked()
    {
        isBeingSucked = true;
    }
}
