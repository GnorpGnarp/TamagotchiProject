using UnityEngine;

public class Foam : MonoBehaviour
{
    private bool isCleaned = false;

    // You can attach the foam to the Tamagotchi if you want to clean it with the shower.
    public void CleanFoam()
    {
        if (!isCleaned)
        {
            isCleaned = true;
            gameObject.SetActive(false);  // Deactivate the foam when cleaned.
            Debug.Log("Foam cleaned!");
        }
    }

    // If you want foam to "react" or do something when in the world.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shower"))
        {
            // Optionally trigger cleaning when shower interacts with foam.
            CleanFoam();
        }
    }
}
