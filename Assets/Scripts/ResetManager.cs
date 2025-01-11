using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public static class ResetManager
{
    public static StopSignChallenge stopSignChallenge; // Reference to StopSignChallenge
    public static CarStopZone carStopZone;             // Reference to CarStopZone

    // Method to initialize challenge references
    public static void InitializeChallenges(StopSignChallenge stopSign, CarStopZone carStop)
    {
        stopSignChallenge = stopSign;
        carStopZone = carStop;
        Debug.Log("Challenges have been initialized in ResetManager.");
    }

    // Static method to reset the car
    public static void ResetCar(Rigidbody carRigidbody, Text feedbackText, string reason)
    {
        if (carRigidbody != null)
        {
            // Hardcoded reset position and rotation
            Vector3 resetPosition = new Vector3(10f, 0.15f, 10f); // Replace with your desired coordinates
            Quaternion resetRotation = Quaternion.Euler(0f, 90f, 0f); // Replace with your desired rotation

            // Reset position and rotation
            carRigidbody.transform.position = resetPosition;
            carRigidbody.transform.rotation = resetRotation;

            // Stop the car's motion
            carRigidbody.linearVelocity = Vector3.zero;
            carRigidbody.angularVelocity = Vector3.zero;

            // Display the feedback
            if (feedbackText != null)
            {
                feedbackText.text = reason;
            }

            // Log the reset
            Debug.Log($"Car has been reset to position {resetPosition} and rotation {resetRotation.eulerAngles}. Reason: {reason}");

            // Reset challenge states
            ResetAllChallenges();

            // Clear feedback after 5 seconds
            if (feedbackText != null)
            {
                ClearFeedback(feedbackText, carRigidbody);
            }
        }
        else
        {
            Debug.LogWarning("ResetCar: Rigidbody is null. Ensure the car's Rigidbody is assigned.");
        }
    }

    // Static helper function to clear feedback text and re-enable car controls after a delay
    private static async void ClearFeedback(Text feedbackText, Rigidbody carRigidbody)
    {
        await Task.Delay(5000); // 5-second delay

        // Clear feedback text
        if (feedbackText != null)
        {
            feedbackText.text = ""; // Clear the feedback text
            Debug.Log("Feedback cleared.");
        }

        // Allow the car to move again
        if (carRigidbody != null)
        {
            carRigidbody.isKinematic = false;
        }
    }

    // Method to reset all challenges
    public static void ResetAllChallenges()
    {
        if (stopSignChallenge != null)
        {
            stopSignChallenge.ResetChallengeState();
        }

        if (carStopZone != null)
        {
            carStopZone.ResetChallengeState();
        }
    }
}
