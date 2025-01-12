using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    public GameObject gameOverScreen;  // Reference to the Game Over Screen Canvas

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
            }
        }
    }

}