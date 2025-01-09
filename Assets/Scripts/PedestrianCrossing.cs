using UnityEngine;

public class PedestrianCrossing : MonoBehaviour
{
    public Transform startPoint; // Pedestrian starting position
    public Transform endPoint;   // Pedestrian ending position
    public float crossingSpeed = 2f; // Speed of pedestrian movement
    private bool isCrossing = false; // Is the pedestrian currently crossing?

    private Animator animator; // Reference to Animator

    void Start()
    {
        // Ensure the pedestrian starts at the startPoint
        transform.position = startPoint.position;
    }

    void Update()
    {
        if (isCrossing)
        {

            // Move pedestrian from startPoint to endPoint
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, crossingSpeed * Time.deltaTime);

            // Stop crossing when the pedestrian reaches the endpoint
            if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
            {
                isCrossing = false;
            }
        }
    }

    // Method to start the crossing
    public void StartCrossing()
    {
        isCrossing = true; // Start the crossing
    }
}
    