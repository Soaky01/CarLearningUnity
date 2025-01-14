using UnityEngine;
using UnityEngine.UI;

public class ChallengeChecker : MonoBehaviour
{
    public PrometeoCarController carController; // Reference to the PrometeoCarController script
    public float speedThreshold = 200f;         // Threshold speed value
    public Text feedbackText;                   // UI Text element for feedback

    private Rigidbody carRigidbody;             // Reference to the car's Rigidbody

    void Update()
    {
        // Check if the car's speed exceeds the threshold
        if (carController != null && carController.carSpeed > speedThreshold)
        {

            // Prevent the car from moving
            carRigidbody = carController.GetComponent<Rigidbody>();
            ResetManager.ResetCar(carRigidbody, feedbackText, "Please do not exceed the speed limit of 50 km/h.");

        }
    }
}