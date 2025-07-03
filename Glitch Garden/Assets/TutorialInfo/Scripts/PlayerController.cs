using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Vector2 moveInput;

    public float moveSpeed = 5f;
    public float rotateSpeed = 100f; // Degrees per second
    private Rigidbody rb;
    private Animator animator;

    public TextMeshProUGUI ScoreText;
    private int numberofplants = 0;

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
        updateScore();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Forward/backward movement input
        float moveAmount = moveInput.y;

        // Left/right turning input
        float turnAmount = moveInput.x;

        // Move forward/backward in the current facing direction
        Vector3 moveDirection = transform.forward * moveAmount;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        // Rotate left/right in place
        if (Mathf.Abs(turnAmount) > 0.1f)
        {
            float turnSpeedThisFrame = turnAmount * rotateSpeed * Time.fixedDeltaTime;
            Quaternion turnOffset = Quaternion.Euler(0f, turnSpeedThisFrame, 0f);
            rb.MoveRotation(rb.rotation * turnOffset);
        }

        // Update Animator
        float currentSpeed = Mathf.Abs(moveAmount) * moveSpeed;
        animator.SetFloat("Speed", currentSpeed);
    }

    private void OnTriggerEnter(Collider other)
{
    Debug.Log($"[Trigger] Banana Man collided with: {other.name}, Tag: {other.tag}");

    if (other.CompareTag("Plant"))
    {
        Debug.Log("[Trigger] It’s a Plant! Picking it up…");
        numberofplants++;
        updateScore();
        Destroy(other.gameObject);
    }
    else
    {
        Debug.Log("[Trigger] Not a Plant — ignoring.");
    }
}


    private void updateScore()
    {
        ScoreText.text = numberofplants.ToString();
    }
}

