using UnityEngine;
using UnityEngine.UI;


public class StopSignChallenge : MonoBehaviour
{
    public float requiredStopTime = 10f; // Time the car must stop
    private float stopTimer = 0f;       // Timer to track stop time
    private bool isCarStopped = false; // To check if the car is stopped
    public Text feedbackText;          // UI Text element for feedback
    private bool isChallangeDone = false;

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("PlayerCar")) // Ensure the object is the car
        {
            Rigidbody carRigidbody = other.GetComponent<Rigidbody>();
            
            if (carRigidbody.linearVelocity.magnitude < 0.1f && !isChallangeDone) // Check if the car is stopped
            {
                stopTimer += Time.deltaTime;
                isCarStopped = true;
                carRigidbody.isKinematic = true; // Freeze the car in place
                // Update feedback UI
                    feedbackText.text = "Stopping at stop signs prevents accidents and keeps everyone on the road safe. Safety starts with you.";

                // Check if the required stop time is met
                if (stopTimer >= requiredStopTime)
                {
                    isChallangeDone = true;
                    carRigidbody.isKinematic = false; // Freeze the car in place

                    // Debug.Log("");
                }
            }
            else
            {
                if(!isChallangeDone)    {

                // Reset the timer if the car starts moving again
                stopTimer = 0f;
                isCarStopped = false;
                feedbackText.text = "Stop at the Stop Sign!";

                }

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            // Reset everything when the car leaves the trigger
            stopTimer = 0f;
            isCarStopped = false;
            feedbackText.text = "";
        }
    }
}
