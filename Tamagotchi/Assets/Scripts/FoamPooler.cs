using System.Collections.Generic;
using UnityEngine;

public class FoamPooler : MonoBehaviour
{
    public GameObject foamPrefab;  // The foam prefab to pool
    public int poolSize = 10;      // Number of foam objects to pool

    private Queue<GameObject> foamPool;  // Queue to hold the pooled foam objects

    void Awake()
    {
        foamPool = new Queue<GameObject>();

        // Pre-instantiate foam objects and add them to the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject foam = Instantiate(foamPrefab);
            foam.SetActive(false);  // Make sure they are inactive initially
            foamPool.Enqueue(foam);
        }
    }

    // Get a foam object from the pool
    public GameObject GetFoam(Vector3 position)
    {
        if (foamPool.Count > 0)
        {
            GameObject foam = foamPool.Dequeue();  // Dequeue an inactive foam
            foam.SetActive(true);  // Activate the foam
            foam.transform.position = position;  // Set the position

            return foam;
        }
        else
        {
            Debug.LogWarning("Foam pool is empty! Consider increasing pool size.");
            return null;
        }
    }

    // Return a foam object back to the pool
    public void ReturnFoamToPool(GameObject foam)
    {
        foam.SetActive(false);  // Deactivate the foam
        foamPool.Enqueue(foam);  // Return it to the pool
    }
}
