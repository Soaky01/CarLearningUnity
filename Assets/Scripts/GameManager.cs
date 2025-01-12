using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isObstacleChallengeCompleted { get; set; } = false;
    public static bool isStopSignChallengeCompleted { get; set; } = false;
    public static bool isPedestrianChallengeCompleted { get; set; } = false;

    public GameObject gameOverScreen; // Reference to the Game Over Screen
  

    void Start()
    {
        // Assign the Game Over Screen to ResetManager
        ResetManager.gameOverScreen = gameOverScreen;

        Debug.Log("Game initialized successfully!");
    }
    public void ResetGame()
    {
        // Call the ResetManager to reset challenges
        ResetManager.ResetAll();
    }
}

