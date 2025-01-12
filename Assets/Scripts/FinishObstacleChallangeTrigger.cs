using UnityEngine;
using UnityEngine.UI; // For using UI elements like Text

public class FinishObstacleChallangeTrigger : MonoBehaviour
{

    public Text feedbackText;
    public string successMessage = "Obstacle Challange Completed! Keep going forward to finish!";

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the tag "PlayerCar"
        if (other.CompareTag("PlayerCar") && !GameManager.isObstacleChallengeCompleted)
        {
            GameManager.isObstacleChallengeCompleted = true;

            // Display the success message in the assigned feedbackText UI
            if (feedbackText != null)
            {
                feedbackText.text = successMessage;
            }
            else
            {
                Debug.LogWarning("FeedbackText is not assigned in the Inspector.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optionally clear the message when the car leaves the trigger zone
        if (other.CompareTag("PlayerCar"))
        {
            if (feedbackText != null)
            {
                feedbackText.text = ""; // Clear the text
            }
        }
    }
}
