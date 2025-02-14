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

        // Debug log for target love value (to check the range of values)
        Debug.Log("Target Love Value: " + targetLoveValue);

        // Gradually interpolate to the target love value over time
        currentLoveValue = Mathf.MoveTowards(currentLoveValue, targetLoveValue, Time.deltaTime * loveMeterSmoothSpeed);

        // Update the slider's value with the smooth value
        loveSlider.value = currentLoveValue;

        // Debug log to check the slider's value (make sure it's updating properly)
        Debug.Log("Slider Value: " + loveSlider.value);

        // Change the slider fill color based on the love value
        if (currentLoveValue >= 70f)
            fillImage.color = Color.magenta; // Pink (happy/neutral)
        else if (currentLoveValue >= 40f)
            fillImage.color = Color.yellow; // Warning
        else
            fillImage.color = Color.blue; // Blue (sad/low)
    }


    // Methods for interactions (feed, play, clean)
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
}
