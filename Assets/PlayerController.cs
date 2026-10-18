using UnityEngine;
using UnityEngine.InputSystem; // Required for the new Input System

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform; // Drag your Main Camera here in the Inspector
    
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Automatically find the Main Camera if reference is left empty
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        if (cameraTransform == null) return;

        // Get the camera's forward and right vectors, and flatten them on the Y axis
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement direction relative to the camera's orientation
        Vector3 moveDirection = (cameraForward * movementY + cameraRight * movementX);

        // Apply force to the rigidbody
        rb.AddForce(moveDirection * speed);
    }
}