using UnityEngine;
using UnityEngine.UI;

public class CarStopZone : MonoBehaviour
{
    private PedestrianCrossing pedestrian;      // Reference to the PedestrianCrossing script
    public Text feedbackText;                   // UI Text element for feedback
    public bool isChallengeDone = false;       // To check if the challenge is done
    public Transform resetFlag;                 // Transform for the reset position and direction
    public float requiredStopTime = 5f;         // Time the car must stop
    public float stopTimer = 0f;               // Timer to track stop time
    public bool isCarStopped = false;          // To check if the car is stopped
    public Rigidbody carRigidbody;             // Reference to the car's Rigidbody
    public PrometeoCarController carController; // Reference to car control script
    public bool isInsideTrigger = false;       // Track if the car is in the trigger

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

        ResetManager.InitializeChallenges(FindObjectOfType<StopSignChallenge>(), this);
    }

    public void ResetChallengeState()
    {
        GameObject pedestrianObject = GameObject.FindWithTag("PedestrianTag"); 
        pedestrian = pedestrianObject.GetComponent<PedestrianCrossing>();
        isChallengeDone = false;
        stopTimer = 0f;
        isCarStopped = false;
        pedestrian.ResetState();
        isInsideTrigger = false;
        isChallengeDone = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            // Assign Rigidbody and PrometeoCarController
            carRigidbody = other.GetComponent<Rigidbody>();
            carController = other.GetComponent<PrometeoCarController>();

            if (carRigidbody != null && carController != null)
            {
                isInsideTrigger = true; // Mark the car as inside the trigger
                Debug.Log("PlayerCar entered the CarStopZone.");
            }
            else
            {
                Debug.LogError("PlayerCar does not have required components (Rigidbody or PrometeoCarController).");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerCar") && isInsideTrigger && carRigidbody != null && carController != null)
        {
            Debug.Log("first if");
            Debug.Log(isChallengeDone); 
            if (!isChallengeDone)
            {
                Debug.Log("second if");
                // Timer logic when pedestrians are not crossing
                if (carRigidbody.linearVelocity.magnitude < 0.1f) // Check if the car is stopped
                {
                    Debug.Log("third if");
                    stopTimer += Time.deltaTime;
                    if (!isCarStopped)
                    {
                        Debug.Log("Fourth if");
                        feedbackText.text = "Please wait while pedestrians cross.";
                        isCarStopped = true;
                        carRigidbody.isKinematic = true; // Freeze the car
                        carController.enabled = false;  // Disable car controls
                    }

                    if (pedestrian != null && !pedestrian.isCrossing)
                    {
                        pedestrian.StartCrossing();
                    }

                    // Check if the required stop time is met and pedestrian has crossed
                    if (stopTimer >= requiredStopTime && pedestrian != null && pedestrian.hasCrossed)
                    {
                        isChallengeDone = true;
                        carRigidbody.isKinematic = false; // Allow the car to move
                        carController.enabled = true;     // Re-enable car controls
                        feedbackText.text = "";
                    }
                }
                else
                {
                    // Reset the timer if the car starts moving again
                    stopTimer = 0f;
                    isCarStopped = false;
                    feedbackText.text = "Stop Before the Crosswalk.";
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            if (isInsideTrigger && !isChallengeDone)
            {
                if (carRigidbody != null && carRigidbody.linearVelocity.magnitude > 0.1f)
                {
                    // Reset only if the car is moving and exits without completing the challenge
                    ResetManager.ResetCar(carRigidbody, feedbackText, "You exited the crosswalk zone without completing the challenge. Please try again.");
                }
            }
            else if (isChallengeDone)
            {
                feedbackText.text = "";
            }

            isInsideTrigger = false; // Mark the car as outside the trigger
            Debug.Log("PlayerCar exited the CarStopZone.");
        }
    }
}
