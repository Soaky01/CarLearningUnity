using UnityEngine;

public class CarStopZone : MonoBehaviour
{
    private PedestrianCrossing pedestrian; // Reference to the PedestrianCrossing script
    public float requiredStopTime = 10f; // Time the car must stop
    private float stopTimer = 0f;       // Timer to track stop time
    private bool isCarStopped = false; // To check if the car is stopped
    public Text feedbackText;          // UI Text element for feedback
    private bool isChallangeDone = false;


    void Start()
    {
        // Find the PedestrianCrossing component by tag "PedestrianTag"
        GameObject pedestrianObject = GameObject.FindWithTag("PedestrianTag");

        if (pedestrianObject != null)
        {
            pedestrian = pedestrianObject.GetComponent<PedestrianCrossing>();

            if (pedestrian == null)
            {
                Debug.LogError("PedestrianCrossing script not found on GameObject with tag 'PedestrianTag'.");
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'PedestrianTag' found in the scene.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the PlayerCar enters the trigger
        if (other.CompareTag("PlayerCar") && !carStopped)
        {
            Rigidbody carRigidbody = other.GetComponent<Rigidbody>();

            if (carRigidbody != null)
            {
                if (carRigidbody.linearVelocity.magnitude < 0.1f && !isChallangeDone) // Check if the car is stopped
                {
                    stopTimer += Time.deltaTime;
                    isCarStopped = true;
                    carRigidbody.isKinematic = true; // Freeze the car in place
                                                     // Update feedback UI
                    feedbackText.text = "Stopping at stop signs prevents accidents and keeps everyone on the road safe. Safety starts with you.";
                    if (pedestrian != null)
                    {
                        pedestrian.StartCrossing();
                        Debug.Log("Pedestrian crossing started.");
                    }

                    // Check if the required stop time is met
                    if (stopTimer >= requiredStopTime)
                    {
                        isChallangeDone = true;
                        carRigidbody.isKinematic = false; // Freeze the car in place
                    }
                }
                else
                {
                    if (!isChallangeDone)
                    {

                        // Reset the timer if the car starts moving again
                        stopTimer = 0f;
                        isCarStopped = false;
                        feedbackText.text = "Stop at the Stop Sign!";

                    }

                }

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the PlayerCar exits the trigger
        if (other.CompareTag("PlayerCar") && carStopped)
        {
            Rigidbody carRigidbody = other.GetComponent<Rigidbody>();

            if (carRigidbody != null)
            {
                carRigidbody.isKinematic = false; // Allow the car to move again
                Debug.Log("Car can move again!");
            }
        }
    }
}
