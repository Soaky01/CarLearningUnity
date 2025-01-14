using UnityEngine;

public class PedestrianCrossing : MonoBehaviour
{
    public Transform startPoint; // Pedestrian starting position
    public Transform endPoint;   // Pedestrian ending position
    public float crossingSpeed = 2f; // Speed of pedestrian movement
    public bool isCrossing = false; // Is the pedestrian currently crossing?
    public bool hasCrossed = false;

    private Animator animator; // Reference to Animator

    void Start()
    {
        // Ensure the pedestrian starts at the startPoint
        transform.position = startPoint.position;

        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on the pedestrian object!");
        }
    }

    void Update()
    {
        if (isCrossing && !GameManager.isPedestrianChallengeCompleted)
        {
            // Move pedestrian from startPoint to endPoint
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, crossingSpeed * Time.deltaTime);

            // Check if the pedestrian has reached the endpoint
            if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
            {
                isCrossing = false;       // Stop crossing
                hasCrossed = true;       // Mark as crossed
                Debug.Log("Setting isWalkingTrigger to false");
                animator.SetBool("isWalkingTrigger", false);
                animator.SetBool("isWalkingTrigger", false); // Stop walking animation
            }
        }
    }


    // Method to start the crossing
    public void StartCrossing()
    {
        isCrossing = true; // Start the crossing

        // Trigger walking animation
        if (animator != null)
        {
            animator.SetBool("isWalkingTrigger", true); // Trigger the walking animation
        }
    }

    public void ResetState()
    {
        if (startPoint != null)
        {
            transform.position = startPoint.position; // Move pedestrian back to the starting point
        }
        hasCrossed = false; // Reset crossing state
        isCrossing = false; // Ensure pedestrian stops moving

        // Reset animation state
        if (animator != null)
        {
            animator.SetBool("isWalkingTrigger", false); // Assuming you use a boolean for walking animation
        }

        Debug.Log("Pedestrian state has been reset.");
    }
}
