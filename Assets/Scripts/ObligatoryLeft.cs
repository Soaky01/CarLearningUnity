using UnityEngine;
using UnityEngine.UI;

public class ObligatoryLeft : MonoBehaviour
{
    public Text feedbackText;          // UI Text element for feedback
    public string triggerTag = "PlayerCar"; // Tag of the object that triggers reset
    private Rigidbody carRigidbody;    // Reference to the car's Rigidbody

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the PlayerCar
        if (other.CompareTag(triggerTag))
        {
            carRigidbody = other.GetComponent<Rigidbody>();

            if (carRigidbody != null)
            {
                // Reset the car's position and direction using ResetManager
                ResetManager.ResetCar(carRigidbody, feedbackText, "You must turn left as instructed. Please try again.");

                // Reset challenge states for all challenges
                ResetManager.ResetAllChallenges();
            }
            else
            {
                Debug.LogWarning("Car Rigidbody is not assigned or null!");
            }
        }
    }
}
