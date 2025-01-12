using UnityEngine;
using UnityEngine.UI;

public class ObstacleTriggerZone : MonoBehaviour
{
    public string playerTag = "PlayerCar"; // Tag for the player car
    public Text feedbackText;             // UI element for displaying feedback
    public string enterMessage = "Avoid the obstacle ahead!"; // Message to display on entering the zone


    private void OnTriggerEnter(Collider other)
    {
        // Check if the player car enters the trigger zone
        if (other.CompareTag(playerTag) && !GameManager.isObstacleChallengeCompleted )
        {
            // Display feedback message
            if (feedbackText != null)
            {
                feedbackText.text = enterMessage;
            }

            // Log the event (for debugging purposes)
            Debug.Log("Player entered the obstacle trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Clear the feedback text when the player leaves the trigger zone
        if (other.CompareTag(playerTag))
        {
            if (feedbackText != null)
            {
                feedbackText.text = "";
            }

            Debug.Log("Player exited the obstacle trigger zone.");
        }
    }
}
