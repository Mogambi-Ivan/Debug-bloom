using UnityEngine;
using UnityEngine.InputSystem; // Needed for the new Input System

public class PlayerController : MonoBehaviour
{
    // Reference to the generated input actions class
    private InputSystem_Actions controls;
    private Vector2 moveInput;

    void Awake()
    {
        // Initialize the input system
        controls = new InputSystem_Actions();
    }

    void OnEnable()
    {
        // Enable the input system
        controls.Enable();

        // Handle movement input
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Add other input handlers (e.g., sprint, jump) here if needed
    }

    void OnDisable()
    {
        // Disable the input system when not in use
        controls.Disable();
    }

    void Start()
    {
        // Any other startup logic goes here
    }

    void Update()
    {
        // Use moveInput here to control the player
        Debug.Log("Move input: " + moveInput);
    }
}
