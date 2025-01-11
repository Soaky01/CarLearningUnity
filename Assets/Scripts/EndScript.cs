using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    public Text feedbackText;          // UI Text element for feedback
    void OnTriggerEnter(Collider other)
    {

        feedbackText.text = "Thank You for Playing.";
    }

}