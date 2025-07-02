using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    // Movement settings
    public float moveSpeed = 5f;
    public float sprintMultiplier = 2f;

    // Jump settings
    public float jumpForce = 5f;
    private bool isGrounded;

    // Components
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Handle movement
        MoveCharacter();

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Physics-based updates can go here if needed
    }

    private void MoveCharacter()
    {
        // Get input from WASD or Arrow Keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Apply movement to the Rigidbody
        if (moveDirection.magnitude >= 0.1f)
        {
            // Apply sprint multiplier if Left Shift is pressed
            float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f);

            rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);

            // Rotate the character to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void Jump()
    {
        // Add force for jumping
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if character is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
