using UnityEngine;

public class Cow : MonoBehaviour
{
    public float shrinkSpeed = 2f;  // Faster shrinking
    public float moveSpeed = 2f;    // Faster movement upwards
    private bool isBeingSucked = false;

    private void Update()
    {
        // Use T to force sucking effect
        if (Input.GetKeyDown(KeyCode.T))  // Press T to test sucking
        {
            StartBeingSucked();
        }

        // If the cow is being sucked, perform shrinking and moving upwards
        if (isBeingSucked)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, shrinkSpeed * Time.deltaTime);
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            // If it's fully shrunk, destroy the cow and give a coin
            if (transform.localScale.x <= 0.05f) // Almost gone
            {
                CoinManager.Instance.AddCoins(1); // Give a coin
                Destroy(gameObject);  // Destroy the cow
            }
        }
    }

    // Start the sucking process
    public void StartBeingSucked()
    {
        Debug.Log(gameObject.name + " is being sucked up!");  // Debug log to confirm it's called
        isBeingSucked = true;
    }
}
