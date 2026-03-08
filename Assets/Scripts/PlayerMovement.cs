using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    float xRotation;
    float gravity = -9.81f;
    float MouseSense = 0.125f;

    [SerializeField] Transform playerCamera;
    [SerializeField] CharacterController controller;

    private Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current != null)
        {
            // .delta reads how much the mouse moved this frame
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            float mouseX = mouseDelta.x * MouseSense;
            float mouseY = mouseDelta.y * MouseSense;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            transform.Rotate(Vector3.up * mouseX);
        }

        // --- 2. WALKING (New Input System Keyboard) ---
        float x = 0f;
        float z = 0f;

        if (Keyboard.current != null)
        {
            // Manually checking WASD keys
            if (Keyboard.current.wKey.isPressed) z += 1f;
            if (Keyboard.current.sKey.isPressed) z -= 1f;
            if (Keyboard.current.dKey.isPressed) x += 1f;
            if (Keyboard.current.aKey.isPressed) x -= 1f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        // This prevents you from moving faster when holding W and D diagonally
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        controller.Move(move * speed * Time.deltaTime);

        // --- 3. GRAVITY ---
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
