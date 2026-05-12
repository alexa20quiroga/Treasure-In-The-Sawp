using UnityEngine;

public class Camaraseguir : MonoBehaviour
{
    // Target that the camera will follow
    public Transform target;

    // Distance between camera and player
    public Vector3 offset = new Vector3(0, 10, -10);

    // Camera smooth movement speed
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        // Stop if no target is assigned
        if (target == null) return;

        // Rotate offset based on player rotation
        Vector3 rotatedOffset = target.rotation * offset;

        // Calculate desired camera position
        Vector3 desiredPosition = target.position + rotatedOffset;

        // Smoothly move camera to desired position
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Make camera look at the player
        transform.LookAt(target);
    }
}