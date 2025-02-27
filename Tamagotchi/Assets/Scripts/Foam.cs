using UnityEngine;

public class Foam : MonoBehaviour
{
    private bool isCleaned = false;

    public FoamPooler foamPooler;  // Reference to the FoamPooler

    void Start()
    {
        if (foamPooler == null)
        {
            Debug.LogError("FoamPooler not assigned!");
        }
    }

    public void CleanFoam()
    {
        if (!isCleaned)
        {
            isCleaned = true;
            gameObject.SetActive(false);  // Deactivate foam when cleaned.
            foamPooler.ReturnFoamToPool(gameObject);  // Return foam to the pool
            Debug.Log("Foam cleaned!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shower"))
        {
            CleanFoam();
        }
    }
}
