using UnityEngine;
using UnityEngine.InputSystem; // Required for the new Input System

public class CameraFollow : MonoBehaviour
{
    public Transform player;            // Drag your Player object here
    public float rotationSpeed = 5f;    // Speed of camera rotation
    public float distance = 6.0f;       // Distance from the player
    public float height = 2.0f;         // Height offset above the player

    private float currentX = 0.0f;
    private float currentY = 15.0f;

    void LateUpdate()
    {
        if (player == null) return;

        // Check if the New Input System's mouse is active and right-click is held down
        if (Mouse.current != null && Mouse.current.rightButton.isPressed)
        {
            // Read mouse movement delta
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            currentX += mouseDelta.x * rotationSpeed * Time.deltaTime;
            currentY -= mouseDelta.y * rotationSpeed * Time.deltaTime;

            // Clamp vertical rotation so the camera doesn't flip upside down
            currentY = Mathf.Clamp(currentY, -5f, 60f);
        }

        // Calculate rotation and new position relative to the player
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 offset = new Vector3(0, height, -distance);
        
        transform.position = player.position + rotation * offset;

        // Always look at the player
        transform.LookAt(player.position + Vector3.up * 1.0f);
    }
}