using UnityEngine;
using UnityEngine.UI;

public class Tamagotchi : MonoBehaviour
{
    public Slider loveSlider;  // Reference to the love meter slider
    public Image fillImage;    // Reference to change the slider's fill color

    private float hunger = 100f;      // Hunger meter (0-100)
    private float fun = 100f;         // Fun meter (0-100)
    private float cleanliness = 100f; // Cleanliness meter (0-100)
    private float currentLoveValue = 100f;  // Store the current love value for smooth transition
    public float decayRate = 0.1f;  // Lower decay rate to make it slower
    public float loveMeterSmoothSpeed = 5f; // Smooth transition speed

    public GameObject foamPrefab;  // Reference to foam prefab
    private GameObject foamInstance; // Hold reference to the foam
    private bool isSoapApplied = false; // Track if soap has been applied

    void Start()
    {
        UpdateLoveMeter(); // Ensure initial setup
    }

    void Update()
    {
        // Gradually decrease the meters over time
        hunger -= decayRate * Time.deltaTime;
        fun -= decayRate * Time.deltaTime;
        cleanliness -= decayRate * Time.deltaTime;

        // Update the love meter based on the current values
        UpdateLoveMeter();
    }

    void UpdateLoveMeter()
    {
        // Calculate the love meter based on hunger, fun, and cleanliness
        float targetLoveValue = (hunger + fun + cleanliness) / 3f;

        // Adjust love based on the individual meters (penalize if any are low)
        if (hunger < 40) targetLoveValue -= 10f;
        if (fun < 40) targetLoveValue -= 10f;
        if (cleanliness < 40) targetLoveValue -= 10f;

        // Clamp the value between 0 and 100
        targetLoveValue = Mathf.Clamp(targetLoveValue, 0f, 100f);

        // Gradually interpolate to the target love value over time
        currentLoveValue = Mathf.MoveTowards(currentLoveValue, targetLoveValue, Time.deltaTime * loveMeterSmoothSpeed);

        // Update the slider's value with the smooth value
        loveSlider.value = currentLoveValue;

        // Change the slider fill color based on the love value
        if (currentLoveValue >= 70f)
            fillImage.color = Color.magenta; // Pink (happy/neutral)
        else if (currentLoveValue >= 40f)
            fillImage.color = Color.yellow; // Warning
        else
            fillImage.color = Color.blue; // Blue (sad/low)
    }
    public void Feed()
    {
        hunger = 100f;
        UpdateLoveMeter();
    }

    public void Play()
    {
        fun = 100f;
        UpdateLoveMeter();
    }

    public void Clean()
    {
        cleanliness = 100f;
        UpdateLoveMeter();
    }
    // Method to apply soap (spawn foam)
    public void ApplySoap(Vector3 spawnPosition)
    {
        if (!isSoapApplied)
        {
            foamInstance = Instantiate(foamPrefab, spawnPosition, Quaternion.identity); // Spawn foam at the specified point
            foamInstance.SetActive(true);  // Activate foam
            isSoapApplied = true;
        }
    }

    // Method to clean foam with showerhead (check for interaction)
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
            foamInstance.SetActive(false); // Deactivate foam (clean it away)
            cleanliness = 100f; // Reset cleanliness
            isSoapApplied = false; // Reset soap applied state
                                   // Optionally, update the cleanliness meter or other logic
        }
    }

}
