using UnityEngine;
using UnityEngine.SceneManagement;  

public class MinigameSceneLoader : MonoBehaviour
{
    // Method to load Minigame1 scene
    public void LoadMinigame1()
    {
        SceneManager.LoadScene("Minigame1");
    }

    // Method to load Minigame2 scene
    public void LoadMinigame2()
    {
        SceneManager.LoadScene("Minigame2");
    }
}
