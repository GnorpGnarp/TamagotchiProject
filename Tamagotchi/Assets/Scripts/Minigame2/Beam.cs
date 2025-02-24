using UnityEngine;

public class Beam : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Beam hit: " + other.name);  // Debug log to confirm collision happens

        if (other.CompareTag("Cow"))
        {
            Debug.Log("Cow detected and should be sucked up: " + other.name);  // Confirm Cow detected

            Cow cow = other.GetComponent<Cow>();
            if (cow != null)
            {
                cow.StartBeingSucked();  // Call the method
            }
            else
            {
                Debug.LogError("Cow script not found on: " + other.name);
            }
        }
    }

}
