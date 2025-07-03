using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Vector2 moveInput;

    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Animator animator;

    void Awake()
    {
        controls = new InputSystem_Actions();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Move
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        // Calculate speed to control animations
        float currentSpeed = moveDirection.magnitude * moveSpeed;

        // Pass speed to Animator
        animator.SetFloat("Speed", currentSpeed);
    }
}

