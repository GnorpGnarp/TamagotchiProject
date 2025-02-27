using UnityEngine;
using UnityEngine.UI;

public class Tamagotchi : MonoBehaviour
{
    public Slider loveSlider;
    public Image fillImage;

    public float hunger = 100f;
    public float fun = 100f;
    public float cleanliness = 100f;
    private float currentLoveValue = 100f;
    public float decayRate = 0.1f;
    public float loveMeterSmoothSpeed = 5f;

    public FoamPooler foamPooler;  // Reference to the FoamPooler
    private GameObject foamInstance;
    private bool isSoapApplied = false;

    void Start()
    {
        UpdateLoveMeter();
    }

    void Update()
    {
        hunger -= decayRate * Time.deltaTime;
        fun -= decayRate * Time.deltaTime;
        cleanliness -= decayRate * Time.deltaTime;
        UpdateLoveMeter();
    }

    void UpdateLoveMeter()
    {
        float targetLoveValue = (hunger + fun + cleanliness) / 3f;
        if (hunger < 40) targetLoveValue -= 10f;
        if (fun < 40) targetLoveValue -= 10f;
        if (cleanliness < 40) targetLoveValue -= 10f;

        targetLoveValue = Mathf.Clamp(targetLoveValue, 0f, 100f);
        currentLoveValue = Mathf.MoveTowards(currentLoveValue, targetLoveValue, Time.deltaTime * loveMeterSmoothSpeed);
        loveSlider.value = currentLoveValue;

        if (currentLoveValue >= 70f)
            fillImage.color = Color.magenta;
        else if (currentLoveValue >= 40f)
            fillImage.color = Color.yellow;
        else
            fillImage.color = Color.blue;
    }

    public void Feed() { hunger = 100f; UpdateLoveMeter(); }
    public void Play() { fun = 100f; UpdateLoveMeter(); }
    public void Clean() { cleanliness = 100f; UpdateLoveMeter(); }

    // Method to apply soap (spawn foam using pooler)
    public void ApplySoap(Vector3 spawnPosition)
    {
        if (!isSoapApplied)
        {
            if (foamInstance != null)
            {
                foamPooler.ReturnFoamToPool(foamInstance);  // Return foam to the pool
            }

            foamInstance = foamPooler.GetFoam(spawnPosition);  // Get foam from pool
            foamInstance.transform.SetParent(this.transform, true); // Attach to Tamagotchi
            foamInstance.SetActive(true);
            isSoapApplied = true;
        }
    }

    public void SetSoapApplied(bool state)
    {
        isSoapApplied = state;
    }

    public Collider2D GetFoamCollider()
    {
        if (foamInstance != null)
            return foamInstance.GetComponent<Collider2D>();
        return null;
    }

    public void CleanFoamWithShower(GameObject showerhead)
    {
        if (foamInstance != null && showerhead != null && foamInstance.activeSelf)
        {
            foamPooler.ReturnFoamToPool(foamInstance);  // Return foam to pool when cleaned
            foamInstance = null;
            isSoapApplied = false;
            cleanliness = 100f;
            UpdateLoveMeter();
        }
    }
}
