using UnityEngine;
using UnityEngine.UI; // If you want to display feedback

public class ObstacleChallenge : MonoBehaviour
{
    public string playerTag = "PlayerCar"; // Tag of the car that triggers the event
    public Text feedbackText;             // UI element for displaying feedback
    
    void Start()
    {
        // Register this challenge with ResetManager
        ResetManager.InitializeChallenges(FindObjectOfType<StopSignChallenge>(), FindObjectOfType<CarStopZone>(), this);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the car collided with the obstacle
        if (collision.gameObject.CompareTag(playerTag))
        {   
            // Log a message to the console
            Debug.Log("Player hit the obstacle!");

            // Optionally: Reset the car or restart the challenge
            ResetManager.ResetCar(
            collision.rigidbody, // Get the car's Rigidbody
            feedbackText,        // Display a reset message
            "You hit an obstacle! Restarting the challenge."
        );
        }

    }

    public void ResetChallengeState()
    {
        GameManager.isObstacleChallengeCompleted = false;
    }
}
