using UnityEngine;
using UnityEngine.UI;

public class StopSignChallenge : MonoBehaviour
{
    public float requiredStopTime = 10f;  // Time the car must stop
    private float stopTimer = 0f;        // Timer to track stop time
    private bool isCarStopped = false;  // To check if the car is stopped
    public Text feedbackText;           // UI Text element for feedback
    private bool isInsideTrigger = false; // Track if the car is in the trigger
    private Rigidbody carRigidbody;       // Reference to the car's Rigidbody



    void Start()
    {
        // Register this challenge with ResetManager
        ResetManager.InitializeChallenges(this, FindObjectOfType<CarStopZone>(), FindObjectOfType<ObstacleChallenge>());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            // Assign the Rigidbody and mark as inside the trigger
            carRigidbody = other.GetComponent<Rigidbody>();
            if (carRigidbody == null)
            {
                Debug.LogError("PlayerCar does not have a Rigidbody component!");
                return;
            }
            isInsideTrigger = true;
        }
    }



    public void ResetChallengeState()
    {
        GameManager.isStopSignChallengeCompleted = false;
        stopTimer = 0f;
        isCarStopped = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerCar") && isInsideTrigger && carRigidbody != null)
        {
            if (!GameManager.isStopSignChallengeCompleted)
            {
                // Check if the car is stopped
                if (carRigidbody.linearVelocity.magnitude < 0.1f)
                {
                    stopTimer += Time.deltaTime;
                    isCarStopped = true;

                    // Update feedback text
                    feedbackText.text = "Stopping at stop signs prevents accidents and keeps everyone on the road safe. \nPlease make a left turn after ensuring there is no incoming traffic.";

                    // Check if the required stop time is met
                    if (stopTimer >= requiredStopTime)
                    {
                        GameManager.isStopSignChallengeCompleted = true;
                        feedbackText.text = "Challenge Complete! You may proceed.";
                    }
                }
                else
                {
                    // Reset the timer if the car moves again
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
            // Ensure the car actually exited and wasn't stopping inside
            if (isInsideTrigger && !GameManager.isStopSignChallengeCompleted)
            {
                if (carRigidbody != null && carRigidbody.linearVelocity.magnitude > 0.1f)
                {
                    // Call the reset function only if the car is moving and exits without completing the challenge
                    ResetManager.ResetCar(carRigidbody, feedbackText, "You exited the stop sign challenge area without stopping properly. Please try again.");
                }
            }
            else if (GameManager.isStopSignChallengeCompleted)
            {
                feedbackText.text = "";
            }
                // Mark as outside the trigger
                isInsideTrigger = false;
        }
    }
}
