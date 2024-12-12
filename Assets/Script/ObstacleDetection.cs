using UnityEngine;

public class ObstacleDetection : MonoBehaviour
{
    public float detectionDistance = 1f;
    public LayerMask obstacleLayer;
    public LayerMask characterLayer; // Add this line

    void Update()
    {
        // Calculate the direction the character is facing
        Vector2 characterDirection = transform.right;

        // Cast a ray in the character's forward direction and get all hits
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, characterDirection, detectionDistance, obstacleLayer);

        // Check if any obstacle is detected
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                // Check if the hit object is not on the character layer
                if (characterLayer != (characterLayer | (1 << hit.collider.gameObject.layer)))
                {
                    Debug.Log("Obstacle detected: " + hit.collider.gameObject.name);
                    // Handle each obstacle blocking the path (e.g., stop character movement, take evasive action, etc.)
                    // For example, you can add a condition to prevent forward movement
                    // YourCharacterMovementScript.CanMoveForward = false;
                }
            }
        }
    }
}
    