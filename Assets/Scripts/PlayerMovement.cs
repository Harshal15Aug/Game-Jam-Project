using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    float xRotation;
    float gravity = -9.81f;
    float MouseSense = 0.13f;

    [SerializeField] Transform playerCamera;
    [SerializeField] CharacterController controller;

    private Vector3 velocity;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        if (Mouse.current != null)
        {
            
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            float mouseX = mouseDelta.x * MouseSense;
            float mouseY = mouseDelta.y * MouseSense;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            transform.Rotate(Vector3.up * mouseX);
        }

        
        float x = 0f;
        float z = 0f;

        if (Keyboard.current != null)
        {
            
            if (Keyboard.current.wKey.isPressed) z += 1f;
            if (Keyboard.current.sKey.isPressed) z -= 1f;
            if (Keyboard.current.dKey.isPressed) x += 1f;
            if (Keyboard.current.aKey.isPressed) x -= 1f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        controller.Move(move * speed * Time.deltaTime);

        
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
