using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isObstacleChallengeCompleted { get; set; } = false;
    public static bool isStopSignChallengeCompleted { get; set; } = false;
    public static bool isPedestrianChallengeCompleted { get; set; } = false;

    public GameObject gameOverScreen; // Reference to the Game Over Screen
    public Rigidbody carRigidbody;   // Reference to the car's Rigidbody


    void Start()
    {
        // Assign the Game Over Screen to ResetManager
        ResetManager.gameOverScreen = gameOverScreen;

        Debug.Log("Game initialized successfully!");
    }
    public void ResetGame()
    {
        // Call the ResetManager to reset challenges
        ResetManager.ResetAll();
        carRigidbody.isKinematic = false;
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
        }
    }
}

