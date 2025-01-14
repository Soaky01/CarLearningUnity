using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public AudioSource engineAudioSource; // Assign the engine audio source in the Inspector
    public GameObject startMenuCanvas; // Assign the StartMenuCanvas in the Inspector
    public GameObject speedometerUI; // Assign the Speedometer UI group in the Inspector
    public  GameObject gameOverScreen;  // Reference to the Game Over Screen Canvas



    void Start()
    {   
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        // Pause the game and mute the engine sound
        Time.timeScale = 0f;
        if (engineAudioSource != null)
        {
            engineAudioSource.mute = true;
        }

        if (speedometerUI != null)
        {
            speedometerUI.SetActive(false);
        }

    
    }

    public void OnNextButtonClicked()
    {
         // Resume the game and unmute the engine sound
        Time.timeScale = 1f;
        startMenuCanvas.SetActive(false);
        if (engineAudioSource != null)
        {
            engineAudioSource.mute = false;
        }
        if (speedometerUI != null)
        {
            speedometerUI.SetActive(true);
        }
    }
}
