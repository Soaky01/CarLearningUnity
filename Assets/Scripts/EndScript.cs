    using UnityEngine;
    using UnityEngine.UI;

    public class EndTrigger : MonoBehaviour
    {
        public GameObject gameOverScreen;  // Reference to the Game Over Screen Canvas
        public Rigidbody rigidBody; // Reference to the Rigidbody of the Player's Car
        public MonoBehaviour playerControllerScript; // Reference to the script controlling player inputs (e.g., PlayerController or similar)


        void OnTriggerEnter(Collider other)
        {

            if(GameManager.isObstacleChallengeCompleted && GameManager.isStopSignChallengeCompleted && GameManager.isPedestrianChallengeCompleted)
            {
                if (other.CompareTag("PlayerCar"))
                {
                    if (gameOverScreen != null)
                    {
                        gameOverScreen.SetActive(true);
                        
                        
                    } else
                    {
                        Debug.LogWarning("Game Over Screen is not assigned in the Inspector.");
                    }
                    if (rigidBody != null)
                    {
                        rigidBody.linearVelocity = Vector3.zero; // Stop linear velocity
                        rigidBody.angularVelocity = Vector3.zero; // Stop rotational velocity
                        rigidBody.isKinematic = true; // Freeze physics on the car
                    }
                }
            }
        }

    }